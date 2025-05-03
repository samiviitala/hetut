# Finnish Social Security Numbers / Suomalainen henkilötunnus

This repository provides C# reference implementations for validating, generating and extracting information contained within Finnish social security numbers.
The code is intentionally not kept DRY, for easy copy-pasting and porting to other languages.

Where applicable, the implementations are kept as simple as possible.

## Valid Finnish social security number is in the following format:
PPKKVVXNNNT
Where:
- PP = day of birth (01-31)
- KK = month of birth (01-12)
- VV = two last digits of year of birth (00-99)
- X = century character indicating century of birth 1800, 1900 or 2000
  - for 1800 century +
  - for 1900 century one of the following: -, Y, X, W, V, U
  - for 2000 century one of the following: A, B, C, D, E, F
- NNN = serial number (002-899)
  - Even for women
  - Odd for men
- Values 900 - 999 are reserved for testing purposes and are not generally considered as valid values
- T = checksum character (0-9, A-Y) which is calculated as PPKKVVNNN % 31 and then reminder dictates the checksum with fixed values seen in _checksumDictionary

> For example 170589-947H is a valid test ssn for a male born in 17.05.1989
 
## 2023 reform 
Prior to year 2023, the century character X was not considered as a distinguishing factor upon assigning social security numbers to persons. Therefore the available namespace was quite limited since same NNN value could not be given to two persons born in same day of the year, but different cenuries.

After the 2023 reform the century character is considered to distinguish two social security numbers from each other. At the same time new century characters were taken to use. For 1900 century new characters Y, X, W, V, U were taken into use. For 2000 century new characters B, C, D, E, F were taken into use.

**Prior to 2023 reform:**
- X = century character indicating century of birth 1800, 1900 or 2000
  - for 1800 century +
  - for 1900 century one of the following: -
  - for 2000 century one of the following: A

**After 2023 reform:**
- X = century character indicating century of birth 1800, 1900 or 2000
  - for 1800 century +
  - for 1900 century one of the following: -, Y, X, W, V, U
  - for 2000 century one of the following: A, B, C, D, E, F

# Usage

Validating social security numbers:
```c#
var validator = new SsnValidator();
validator.Validate("170589-947H"); // true
validator.Validate("ABC123");      //false
```

Generating social security numbers:
```c#
var generator = new SsnGenerator();
generator.Generate(); // e.g. 170589-947H

// we can also take control of what kind of ssns we want to generate
var options = SsnGeneratorOptions.Create(
            seed: 123,
            isTestSsn: true,
            genders: new[] { Gender.Female, Gender.Male },
            dateOfBirthMin: new DateOnly(2020, 1, 1),
            dateOfBirthMax: new DateOnly(2023, 1, 1),
            includeReform2023CenturyCharacters: true
        );
var generator = new SsnGenerator(options); // pass options as arg for the ctor
generator.Generate();
```

Extracting information from social security numbers:
```c#
var extractor = new SsnExtractor();
extractor.TryExtract("170589-947H", out var info); // returns true for valid ssn
// info.DateOfBirth == new DateOnly(1989, 5, 17)
// info.Gender == Gender.Male
// info.IsTestSsn == true
// info.IsAfter2023Reform == false
```

# Performance

Example benchmark execution. Note that we could do a lot better, given we want to squeeze more perf with e.g. C# language specific quirks.
For any practical purposes, this level of performance should suffice though.

> BenchmarkDotNet v0.14.0, Windows 11
13th Gen Intel Core i9-13900K, 1 CPU, 32 logical and 24 physical cores .NET SDK 8.0.206


| Method                  | Mean      | Error    | StdDev   | Gen0   | Allocated |
|------------------------ |----------:|---------:|---------:|-------:|----------:|
| ValidateValid           |  23.77 ns | 0.101 ns | 0.089 ns | 0.0068 |     128 B |
| ValidateInvalidChecksum |  23.79 ns | 0.177 ns | 0.157 ns | 0.0068 |     128 B |
| Generate                | 101.60 ns | 0.245 ns | 0.217 ns | 0.0038 |      72 B |
| ExtractValid            |  27.14 ns | 0.149 ns | 0.132 ns | 0.0085 |     160 B |
| ExtractInvalidChecksum  |  24.06 ns | 0.492 ns | 0.411 ns | 0.0068 |     128 B |

## Reference implementation
There are multiple improvements that could be implemented in the C# implementation to improve performance, memory usage, and overall code quality. 
However, this would defeat the purpose of acting as a simple reference implementation which can easily be ported to another languages.
We could for example in the validator use memory spans instead of substrings but these C# specific quirks will not translate to other languages as easily.

## Contributing
If you have any suggestions or improvements, please feel free to open an issue or submit a pull request. Contributions are welcome!

I'm interested in adding more languages, so if you have a working implementation in another language e.g. Java, JavaScript, SQL etc... , please feel free to submit a pull request.

Maybe we'll provide npm, NuGet and other packages in the future.

## Useful links
- https://hetut.fi/
- https://dvv.fi/henkilotunnus
- https://dvv.fi/hetu-uudistus
- https://www.suomi.fi/kansalaiselle/oikeudet-ja-velvollisuudet/lainsaadanto-ja-oikeusturva/opas/henkilotiedot-ja-niiden-kasittely/henkilotunnus
- https://stat.fi/meta/kas/hetu.html
- https://tietosuoja.fi/usein-kysyttya-henkilotunnus
- https://tarkistusmerkit.teppovuori.fi/tarkmerk.htm#hetu1
- https://telepartikkeli.azurewebsites.net/tunnusgeneraattori
- https://www.lintukoto.net/muut/henkilotunnus/

## Author
Sami Viitala
sami.viitala@devved.fi

## License
MIT