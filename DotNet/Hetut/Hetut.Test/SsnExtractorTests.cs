using System.Diagnostics.CodeAnalysis;
using Hetut.Test.TestCases;

namespace Hetut.Test;

[TestFixture]
[ExcludeFromCodeCoverage]
public class SsnExtractorTests
{
    [TestCaseSource(typeof(SsnTestCases), nameof(SsnTestCases.ValidSsnTestCases))]
    public void TryExtract_GivenValidSSN_ReturnsTrue(string? ssn)
    {
        var sut = new SsnExtractor();
        var result = sut.TryExtract(ssn, out _);
        Assert.That(result, Is.True, $"Expected true for valid SSN: {ssn}");
    }
    
    [TestCaseSource(typeof(SsnTestCases), nameof(SsnTestCases.InvalidSsnTestCases))]
    public void TryExtract_GivenInvalidSSN_ReturnsFalse(string? ssn)
    {
        var sut = new SsnExtractor();
        var result = sut.TryExtract(ssn, out _);
        Assert.That(result, Is.False, $"Expected false for invalid SSN: {ssn}");
    }

    [TestCaseSource(typeof(SsnExtractorTestCases), nameof(SsnExtractorTestCases.DateOfBirthTestCases))]
    public DateOnly TryExtract_GivenValidSSN_ExtractsDateOfBirth(string ssn)
    {
        var sut = new SsnExtractor();
        sut.TryExtract(ssn, out var result);
        return result!.DateOfBirth;
    }
    
    [TestCaseSource(typeof(SsnExtractorTestCases), nameof(SsnExtractorTestCases.GenderTestCases))]
    public Gender TryExtract_GivenValidSSN_ExtractsGender(string ssn)
    {
        var sut = new SsnExtractor();
        sut.TryExtract(ssn, out var result);
        return result!.Gender;
    }
    
    [TestCaseSource(typeof(SsnExtractorTestCases), nameof(SsnExtractorTestCases.IsTestSSNTestCases))]
    public bool TryExtract_GivenValidSSN_ExtractsIsTestSSN(string ssn)
    {
        var sut = new SsnExtractor();
        sut.TryExtract(ssn, out var result);
        return result!.IsTestSsn;
    }
}