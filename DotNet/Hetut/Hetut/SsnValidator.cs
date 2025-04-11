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

/// <inheritdoc cref="ISsnValidator"/> 
public class SsnValidator : ISsnValidator
{
    /// <summary>
    /// Dictionary of valid checksum characters for Finnish social security number.
    /// </summary>
    private static readonly Dictionary<int, char> ChecksumDictionary = new()
    {
        { 0, '0' },
        { 1, '1' },
        { 2, '2' },
        { 3, '3' },
        { 4, '4' },
        { 5, '5' },
        { 6, '6' },
        { 7, '7' },
        { 8, '8' },
        { 9, '9' },
        { 10, 'A' },
        { 11, 'B' },
        { 12, 'C' },
        { 13, 'D' },
        { 14, 'E' },
        { 15, 'F' },
        { 16, 'H' },
        { 17, 'J' },
        { 18, 'K' },
        { 19, 'L' },
        { 20, 'M' },
        { 21, 'N' },
        { 22, 'P' },
        { 23, 'R' },
        { 24, 'S' },
        { 25, 'T' },
        { 26, 'U' },
        { 27, 'V' },
        { 28, 'W' },
        { 29, 'X' },
        { 30, 'Y' }
    };
    
    public bool Validate(string? ssn)
    {
        /*
         * Valid Finnish social security number is in the format:
         * PPKKVVXNNNT
         * Where:
         * - PP = day of birth (01-31)
         * - KK = month of birth (01-12)
         * - VV = two last digits of year of birth (00-99)
         * - X = century separator character (A-F, Y-U, +) indicating century of birth 1800, 1900 or 2000
         * - NNN = serial number (002-899)
         *   - Even for women
         *   - Odd for men
         *   - Values 900 - 999 are reserved for testing purposes and are not generally considered as valid values
         * - T = checksum character (0-9, A-Y) which is calculated as PPKKVVNNN % 31 and then reminder dictates the checksum with fixed values seen in _checksumDictionary
         */
        
        // Empty ssn is obviously invalid
        if(string.IsNullOrEmpty(ssn))
        {
            return false;
        }
        
        // Finnish ssn is 11 characters long
        if(ssn.Length != 11)
        {
            return false;
        }
        
        var pp = ssn[..2];
        var kk = ssn[2..4];
        var vv = ssn[4..6];

        if (!int.TryParse(pp, out var day))
        {
            return false;
        }
        if (!int.TryParse(kk, out var month))
        {
            return false;
        }
        if (!int.TryParse(vv, out var year))
        {
            return false;
        }

        if (!TryParseCentury(ssn[6], out var century))
        {
            return false;
        }
        
        // Check if the date is valid
        var yearOfBirth = century + year;
        try
        {
            _ = new DateOnly(yearOfBirth, month, day);
        }
        catch (ArgumentOutOfRangeException)
        {
            return false;
        }
        var nnnStr = ssn[7..10];
        if (!int.TryParse(nnnStr, out var nnn) || nnn < 2 || nnn > 999)
        {
            return false;
        }
        var ppkkvvnnn = int.Parse(pp + kk + vv + nnnStr);
        var checksumChar = ssn[10];
        if(!TryCalculateChecksum(ppkkvvnnn, out var calculatedChecksumChar) || 
           checksumChar != calculatedChecksumChar)
        {
            return false;
        }

        return true;
    }
        

    /// <summary>
    /// Finnish Social security number allows following century separator characters:
    ///
    /// <list type="bullet">
    ///<item>A, B, C, D, E, F for 2000s</item>
    ///<item>Y, X, W, V, U for 1900s</item>
    ///<item>+ for 1800s</item>
    /// </list>
    /// </summary>
    /// <param name="centuryChar">Century character</param>
    /// <param name="century">Output variable for parsed century: 1800 or 1900 or 2000 for valid century, -1 otherwise</param>
    /// <returns>True if valid century character, false otherwise</returns>
    private static bool TryParseCentury(char centuryChar, out int century)
    {
        switch (centuryChar)
        {
            case 'A':
            case 'B':
            case 'C':
            case 'D':
            case 'E':
            case 'F':
                century = 2000;
                return true;
            case 'Y':
            case 'X':
            case 'W':
            case 'V':
            case 'U':
                century = 1900;
                return true;
            case '+':
                century = 1800;
                return true;
            default:
                century = -1;
                return false;
        }
    }
    
    /// <summary>
    /// Try parse/calculate the checksum character for the given ppkkvvnnn part of Finnish social security number.
    /// </summary>
    /// <param name="ppkkvvnnn">ppkkvvnnn from ssn</param>
    /// <param name="checksumChar">Output variable for parsed checksum char for valid ssns, otherwise default</param>
    /// <returns>True if </returns>
    private static bool TryCalculateChecksum(int ppkkvvnnn, out char checksumChar)
    {
        var checksum = ppkkvvnnn % 31;
        return ChecksumDictionary.TryGetValue(checksum, out checksumChar);
    }
}
