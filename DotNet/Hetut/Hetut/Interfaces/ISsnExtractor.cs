using System.Diagnostics.CodeAnalysis;

namespace Hetut;

/// <summary>
///     Extracts information contained within the Finnish social security number.
/// </summary>
public interface ISsnExtractor
{
    /// <summary>
    /// Try to extract the information from the given Finnish social security number.
    /// </summary>
    /// <param name="ssn">Finnish social security number to extract the information from.</param>
    /// <param name="ssnInformation">Extracted information contained within the SSN if SSN was valid, null otherwise</param>
    /// <returns>True if SSN is valid, false otherwise</returns>
    bool TryExtract([NotNullWhen(true)] string? ssn, [NotNullWhen(true)] out SsnInformation? ssnInformation);
}