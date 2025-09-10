namespace YTunnus;

/// <summary>
///     Generator for Finnish business ids (Y-Tunnus)
/// </summary>
public interface IBusinessIdGenerator
{
    /// <summary>
    ///     Generate a Finnish business id
    /// </summary>
    /// <returns></returns>
    string Generate();
}