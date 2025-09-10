using System.Diagnostics.CodeAnalysis;

namespace YTunnus;

/// <summary>
///     Validator for Finnish business ids (Y-Tunnus)
/// </summary>
public interface IBusinessIdValidator
{
    /// <summary>
    ///     Validate Finnish business id
    /// </summary>
    /// <param name="businessId">Business id, e.g. 1234567-1</param>
    /// <returns>True when business id is valid, false otherwise</returns>
    bool Validate([NotNullWhen(true)] string? businessId);
}