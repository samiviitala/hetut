using System.Diagnostics.CodeAnalysis;

namespace Hetut.Test;

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
}