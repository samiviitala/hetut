namespace YTunnus.Test;

public class BusinessIdValidatorTests
{

    [TestCase("3280643-9", Description = "Devved Oy")]
    [TestCase("1927400-1", Description = "KONE Oyj")]
    [TestCase("0112038-9", Description = "Nokia Oyj")]
    [TestCase("2336509-6", Description = "Supercell Oy")]
    public void Validate_GivenValidBusinessId_ReturnsTrue(string businessId)
    {
        var sut = new BusinessIdValidator();
        var isValid = sut.Validate(businessId);
        Assert.That(isValid, Is.True);
    }

    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    [TestCase("0")]
    [TestCase("32806439", Description = "Devved Oy, but omitting dash is not allowed")]
    [TestCase(" 3280643-9", Description = "Devved Oy, but leading white-space is not allowed")]
    [TestCase("3280643-9 ", Description = "Devved Oy, but trailing white-space is not allowed")]
    [TestCase("3280643-8 ", Description = "Devved Oy, but checksum digit is incorrect")]
    [TestCase("3280642-9 ", Description = "Devved Oy, but of the nnnnnn digits incorrect")]
    public void Validate_GivenInvalidBusinessId_ReturnsFalse(string? businessId)
    {
        var sut = new BusinessIdValidator();
        var isValid = sut.Validate(businessId);
        Assert.That(isValid, Is.False);
    }
}