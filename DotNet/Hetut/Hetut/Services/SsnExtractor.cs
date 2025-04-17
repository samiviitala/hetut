using System.Diagnostics.CodeAnalysis;

namespace Hetut;

/// <inheritdoc />
public class SsnExtractor : ISsnExtractor
{
    /// <summary>
    ///     Valid checksum characters for Finnish social security number.
    /// </summary>
    private static readonly char[] ChecksumChars = 
    {
        '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 
        'A', 'B', 'C', 'D', 'E', 'F', 'H', 'J', 'K', 'L', 
        'M', 'N', 'P', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y'
    };

    /// <inheritdoc />
    public bool TryExtract(string? ssn, [NotNullWhen(true)] out SsnInformation? ssnInformation)
    {
        if (string.IsNullOrEmpty(ssn))
        {
            ssnInformation = null;
            return false;
        }

        // Finnish ssn is 11 characters long
        if (ssn.Length != 11)
        {
            ssnInformation = null;
            return false;
        }

        var pp = ssn[..2];
        var kk = ssn[2..4];
        var vv = ssn[4..6];

        if (!int.TryParse(pp, out var day))
        {
            ssnInformation = null;
            return false;
        }

        if (!int.TryParse(kk, out var month))
        {
            ssnInformation = null;
            return false;
        }

        if (!int.TryParse(vv, out var year))
        {
            ssnInformation = null;
            return false;
        }

        // Validate century part of ssn
        var centuryChar = ssn[6];
        int century;
        switch (centuryChar)
        {
            case 'A':
            case 'B':
            case 'C':
            case 'D':
            case 'E':
            case 'F':
                century = 2000;
                break;
            case 'Y':
            case 'X':
            case 'W':
            case 'V':
            case 'U':
            case '-':
                century = 1900;
                break;
            case '+':
                century = 1800;
                break;
            default:
                ssnInformation = null;
                return false;
        }

        // Check if the date is valid
        var yearOfBirth = century + year;
        DateOnly dateOfBirth;
        try
        {
            dateOfBirth = new DateOnly(yearOfBirth, month, day);
        }
        catch (ArgumentOutOfRangeException)
        {
            ssnInformation = null;
            return false;
        }

        // Validate nnn part of ssn
        var nnnStr = ssn[7..10];
        if (!int.TryParse(nnnStr, out var nnn) || nnn < 2 || nnn > 999)
        {
            ssnInformation = null;
            return false;
        }
        
        // Check if the nnn part is valid
        var ppkkvvnnn = day * 10000000 + 
                        month * 100000 + 
                        year * 1000 + 
                        nnn;
        var ssnChecksum = ssn[10];

        // Calculate and validate the checksum
        var reminder = ppkkvvnnn % 31;
        var correctChecksum = ChecksumChars[reminder];

        if (ssnChecksum != correctChecksum)
        {
            ssnInformation = null;
            return false;
        }

        var gender = nnn % 2 == 0 ? Gender.Female : Gender.Male;
        var isTestSsn = nnn >= 900;
        var isAfter2023Reform = centuryChar is 'B' or 'C' or 'D' or 'E' or 'F' or 'Y' or 'X' or 'W' or 'V' or 'U';
        ssnInformation = new SsnInformation(dateOfBirth, gender, isTestSsn, isAfter2023Reform);
        return true;
    }
}