namespace YTunnus.Test;

public class BusinessIdGeneratorTests
{
    [Test]
    public void Generate_ReturnsValidBusinessId()
    {
        var sut = new BusinessIdGenerator();
        var validator = new BusinessIdValidator();
        var isValid = validator.Validate(sut.Generate());
        Assert.That(isValid, Is.True);
    }
    
    /// <summary>
    /// The available namespace of business ids is where nnnnnnn is between [0100000, 9999999]
    /// (dash and checksum don't count towards available space)
    /// So  9_899_999 business ids in total. We shall
    /// 
    /// </summary>
    /// <param name="generateCount"></param>
    [TestCase(9_899_999)]
    public void Generate_InSequence_ReturnsUniqueBusinessIds(int generateCount)
    {
        var sut = new BusinessIdGenerator();

        var hashset = new HashSet<string>();
        for (var i = 0; i < generateCount; i++)
        {
            var businessId = sut.Generate();
            if (!hashset.Add(businessId))
            {
                Assert.Fail($"BusinessId {businessId} had already been previously generated");
            }
        }
    }
}