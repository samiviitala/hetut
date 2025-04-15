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
        Assert.That(result, Is.True);
    }
    
    [TestCaseSource(typeof(SsnTestCases), nameof(SsnTestCases.InvalidSsnTestCases))]
    public void TryExtract_GivenInvalidSSN_ReturnsFalse(string? ssn)
    {
        var sut = new SsnExtractor();
        var result = sut.TryExtract(ssn, out _);
        Assert.That(result, Is.False);

    }
}