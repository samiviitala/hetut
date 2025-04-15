using System.Diagnostics.CodeAnalysis;
using Hetut.Test.TestCases;

namespace Hetut.Test;

[TestFixture]
[ExcludeFromCodeCoverage]
public class SsnValidatorTests
{
   
    [TestCaseSource(typeof(SsnTestCases), nameof(SsnTestCases.ValidSsnTestCases))]
    public void Validate_GivenValidSsn_ReturnsTrue(string ssn)
    {
        var sut = new SsnValidator();
        var result = sut.Validate(ssn);
        Assert.That(result, Is.True, $"Expected true for valid SSN: {ssn}");
    }
    
    [TestCaseSource(typeof(SsnTestCases), nameof(SsnTestCases.InvalidSsnTestCases))]
    public void Validate_GivenInvalidSsn_ReturnsFalse(string ssn)
    {
        var sut = new SsnValidator();
        var result = sut.Validate(ssn);
        Assert.That(result, Is.False, $"Expected false for invalid SSN: {ssn}");
    }
}