using System.Diagnostics.CodeAnalysis;

namespace Hetut.Test;

/// <summary>
///     Tests for the SsnGenerator make use of both SsnValidator and SsnExtractor.
///     As long as their implementation is correct, were golden.
/// </summary>
[TestFixture]
[ExcludeFromCodeCoverage]
public class SsnGeneratorTests
{
    [Test]
    public void Generate_WithoutSeed_ReturnsValidSSN()
    {
        var sut = new SsnGenerator();
        var ssn = sut.GenerateSsn();

        var validator = new SsnValidator();
        var isValid = validator.Validate(ssn);
        Assert.That(isValid, Is.EqualTo(true));
    }
    
    [TestCase(0)]
    [TestCase(1)]
    [TestCase(123)]
    [TestCase(987654321)]
    public void Generate_WithSeed_ReturnsValidSSN(int seed)
    {
        var sut = new SsnGenerator(SsnGeneratorOptions.Create(seed));
        var ssn = sut.GenerateSsn();

        var validator = new SsnValidator();
        var isValid = validator.Validate(ssn);
        Assert.That(isValid, Is.EqualTo(true));
    }

    [TestCase(0)]
    [TestCase(1)]
    [TestCase(123)]
    [TestCase(987654321)]
    public void Generate_WithSameSeed_ReturnsSameSSN(int seed)
    {
        var sut1 = new SsnGenerator(SsnGeneratorOptions.Create(seed));
        var sut2 = new SsnGenerator(SsnGeneratorOptions.Create(seed));

        const int count = 10;
        List<string> ssns1 = new();
        List<string> ssns2 = new();
        
        for(var i = 0; i < count; i++)
        {
            ssns1.Add(sut1.GenerateSsn());
            ssns2.Add(sut2.GenerateSsn());
        }
        
        Assert.That(ssns1, Is.EqualTo(ssns2));
    }

    [TestCase(true)]
    [TestCase(false)]
    public void Generate_WithTypes_ReturnsSsnWithTypes(bool isTestSsn)
    {
        var sut = new SsnGenerator(SsnGeneratorOptions.Create(isTestSsn: isTestSsn));
        var ssnExtractor = new SsnExtractor();
        
        for(var i=0; i<100; i++)
        {
            var ssn = sut.GenerateSsn();
            ssnExtractor.TryExtract(ssn, out var ssnInformation);
            Assert.That(ssnInformation!.IsTestSsn, Is.EqualTo(isTestSsn));
        }
    }

    [Test]
    public void Generate_WithEmptyGenders_ThrowsInvalidOperationsException()
    {
        var sut = new SsnGenerator(SsnGeneratorOptions.Create(genders: Array.Empty<Gender>()));
        Assert.Throws<InvalidOperationException>(() => sut.GenerateSsn());
    }
    
    [TestCase(Gender.Male)]
    [TestCase(Gender.Female)]
    public void Generate_WithSingleGender_ReturnsSsnWithGender(Gender gender)
    {
        var sut = new SsnGenerator(SsnGeneratorOptions.Create(genders: new []{ gender }));
        var ssnExtractor = new SsnExtractor();
        
        for(var i=0; i<100; i++)
        {
            var ssn = sut.GenerateSsn();
            ssnExtractor.TryExtract(ssn, out var ssnInformation);
            Assert.That(ssnInformation!.Gender, Is.EqualTo(gender), message: $"Expected ssn with gender {gender}, but got ssn {ssn} whose nnn part indicates wrong gender");
        }
    }
    
    /// <summary>
    ///     Use fixed seed which is known to generate a result over the confidence level
    /// </summary>
    [TestCase(123)]
    public void Generate_WithTwoGenders_ReturnsEvenDistributionOfMaleAndFemaleSsns(int seed)
    {
        const int numberOfSamples = 10000;
        var maleCount = 0;
        var femaleCount = 0;
    
        // Your SSN generator
        var ssnGenerator = new SsnGenerator(SsnGeneratorOptions.Create(seed, genders: new [] { Gender.Female, Gender.Male}));
        var ssnExtractor = new SsnExtractor();
    
        // Generate samples and count
        for (var i = 0; i < numberOfSamples; i++)
        {
            var ssn = ssnGenerator.GenerateSsn();
            if (!ssnExtractor.TryExtract(ssn, out var ssnInformation))
            {
                Assert.Fail("Generated SSN is invalid");
                return;
            }
        
            if (ssnInformation.Gender == Gender.Male)
                maleCount++;
            else
                femaleCount++;
        }
    
        // Expected count for even distribution
        var expectedCount = numberOfSamples / 2.0;
    
        // Calculate chi-square statistic
        var chiSquare = Math.Pow(maleCount - expectedCount, 2) / expectedCount + 
                        Math.Pow(femaleCount - expectedCount, 2) / expectedCount;
    
        // Critical value for chi-square with 1 degree of freedom at 95% confidence
        const double criticalValue = 3.841; // For alpha = 0.05 with 1 degree of freedom
    
        Console.WriteLine($"Male count: {maleCount}, Female count: {femaleCount}");
        Console.WriteLine($"Chi-square value: {chiSquare}, Critical value: {criticalValue}");
        Console.WriteLine($"Distribution is {(chiSquare < criticalValue ? "even" : "not even")}");
    
        // This could be an assert in a unit test
        Assert.That(chiSquare, Is.LessThan(criticalValue));
    }
    
    [TestCase(0, 1)]
    [TestCase(123, 456)]
    [TestCase(987654321, 638646384)]
    public void Generate_WithDifferentSeed_ReturnsDifferentSSN(int seed1, int seed2)
    {
        var sut1 = new SsnGenerator(SsnGeneratorOptions.Create(seed1));
        var sut2 = new SsnGenerator(SsnGeneratorOptions.Create(seed2));

        const int count = 10;
        List<string> ssns1 = new();
        List<string> ssns2 = new();
        
        for(var i = 0; i < count; i++)
        {
            ssns1.Add(sut1.GenerateSsn());
            ssns2.Add(sut2.GenerateSsn());
        }
        
        Assert.That(ssns1, Is.Not.EqualTo(ssns2));
    }

    [Test, TestCaseSource(nameof(ValidSsnDates))]
    public void Generate_WithDateOfBirthMin_GeneratesSsnsWithDateOfBirthGreaterOrEqual(DateOnly dateOfBirthMin)
    {
        var sut = new SsnGenerator(SsnGeneratorOptions.Create(dateOfBirthMin: dateOfBirthMin));
        var extractor = new SsnExtractor();
        
        const int count = 1000;
        for (var i = 0; i < count; i++)
        {
            var ssn = sut.GenerateSsn();
            extractor.TryExtract(ssn, out var info);
            Assert.That(info!.DateOfBirth, Is.GreaterThanOrEqualTo(dateOfBirthMin));
        }
    }
    
    [Test, TestCaseSource(nameof(ValidSsnDates))]
    public void Generate_WithDateOfBirthMax_GeneratesSsnsWithDateOfBirthLessOrEqual(DateOnly dateOfBirthMax)
    {
        var sut = new SsnGenerator(SsnGeneratorOptions.Create(dateOfBirthMax: dateOfBirthMax));
        var extractor = new SsnExtractor();
        
        const int count = 1000;
        for (var i = 0; i < count; i++)
        {
            var ssn = sut.GenerateSsn();
            extractor.TryExtract(ssn, out var info);
            Assert.That(info!.DateOfBirth, Is.LessThanOrEqualTo(dateOfBirthMax));
        }
    }

    [Test, TestCaseSource(nameof(ValidSsnDates))]
    public void Generate_WithEqualDateOfBirthMinAndDateOfBirthMax_GeneratesSsnWithFixedDate(DateOnly date)
    {
        var sut = new SsnGenerator(SsnGeneratorOptions.Create(dateOfBirthMin: date, dateOfBirthMax: date));
        var extractor = new SsnExtractor();
        
        const int count = 1000;
        for (var i = 0; i < count; i++)
        {
            var ssn = sut.GenerateSsn();
            extractor.TryExtract(ssn, out var info);
            Assert.That(info!.DateOfBirth, Is.EqualTo(date));
        }
    }

    [Test]
    public void Generate_WithIncludeReform2023CenturyCharactersFalse_GeneratesSsnWithoutReformCenturyCharacters()
    {
        var sut = new SsnGenerator(SsnGeneratorOptions.Create(includeReform2023CenturyCharacters: false));
        var extractor = new SsnExtractor();
        
        const int count = 1000;
        for (var i = 0; i < count; i++)
        {
            var ssn = sut.GenerateSsn();
            extractor.TryExtract(ssn, out var info);
            Assert.That(info!.IsAfter2023Reform, Is.EqualTo(false));
        }
    }
    
    private static IEnumerable<TestCaseData> ValidSsnDates
    {
        get
        {
            yield return new TestCaseData(new DateOnly(1800,1,1));
            yield return new TestCaseData(new DateOnly(1800,12,31));
            yield return new TestCaseData(new DateOnly(1868,4,2));
            yield return new TestCaseData(new DateOnly(1899,12,31));
            yield return new TestCaseData(new DateOnly(1900,1,1));
            yield return new TestCaseData(new DateOnly(1900,12,31));
            yield return new TestCaseData(new DateOnly(1938,2,12));
            yield return new TestCaseData(new DateOnly(1999,12,31));
            yield return new TestCaseData(new DateOnly(2000,1,1));
            yield return new TestCaseData(new DateOnly(2020,5,7));
        }
    }
}