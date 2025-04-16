# Finnish Social Security Numbers

This repository provides C# reference implementations for validating, generating and extracting information contained within Finnish social security numbers.
The code is intentionally not kept DRY, for easy copy-pasting and porting to other languages.

Where possible, the implementations are kept as simple as possible as single functions.

## Valid Finnish social security number is in the following format:
PPKKVVXNNNT
Where:
- PP = day of birth (01-31)
- KK = month of birth (01-12)
- VV = two last digits of year of birth (00-99)
- X = century separator character (A-F, Y-U, +) indicating century of birth 1800, 1900 or 2000
- NNN = serial number (002-899)
  - Even for women
  - Odd for men
- Values 900 - 999 are reserved for testing purposes and are not generally considered as valid values
- T = checksum character (0-9, A-Y) which is calculated as PPKKVVNNN % 31 and then reminder dictates the checksum with fixed values seen in _checksumDictionary

## Contributing
If you have any suggestions or improvements, please feel free to open an issue or submit a pull request. Contributions are welcome!

I'm interested in adding more languages, so if you have a working implementation in another language e.g. Java and SQL, please feel free to submit a pull request.

## License
MIT
