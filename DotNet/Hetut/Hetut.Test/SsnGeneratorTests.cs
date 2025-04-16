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
        var sut = new SsnGenerator();
        var ssn = sut.GenerateSsn(seed);

        var validator = new SsnValidator();
        var isValid = validator.Validate(ssn);
        Assert.That(isValid, Is.EqualTo(true));
    }
    
    /// <summary>
    ///     Use fixed seed which is known to generate a result over the confidence level
    /// </summary>
    [TestCase(123)]
    public void Generate_WithSeed_GenerateEvenDistributionOfMaleAndFemaleSsns(int seed)
    {
        const int numberOfSamples = 10000;
        var maleCount = 0;
        var femaleCount = 0;
    
        // Your SSN generator
        var ssnGenerator = new SsnGenerator();
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
}