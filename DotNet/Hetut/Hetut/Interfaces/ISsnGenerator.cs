namespace Hetut;

/// <summary>
///     Generates a valid Finnish social security number.
/// </summary>
public interface ISsnGenerator
{
    /// <summary>
    ///     Generates a valid Finnish social security number.
    /// </summary>
    /// <returns>Valid Finnish social security number</returns>
    public string GenerateSsn();
}