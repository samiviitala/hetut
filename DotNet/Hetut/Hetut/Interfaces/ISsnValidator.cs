using System.Diagnostics.CodeAnalysis;

namespace Hetut;

/// <summary>
///     Validator for Finnish social security number.
/// </summary>
public interface ISsnValidator
{
    /// <summary>
    ///     Validates the given Finnish social security number.
    ///     This method efficiently checks the format and structure of the SSN, only returning a boolean value indicating validity of given SSN.
    ///     <remarks>
    ///     Considers test SSNs where NNN is 900-999 as valid.
    ///     </remarks>
    /// </summary>
    /// <param name="ssn">Finnish social security number to validate.</param>
    /// <returns>True if the given Finnish social security number is valid; otherwise, false.</returns>
    bool Validate([NotNullWhen(true)] string? ssn);
}