namespace Hetut;

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
    
    /// <inheritdoc cref="ISsnValidator"/> 
    public bool Validate(string? ssn)
    {
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
        
        // Validate ppkkvv part of the ssn
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
            case '-': // TODO: Add unit test cases for - case
                century = 1900;
                break;
            case '+':
                century = 1800;
                break;
            default:
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
        
        // Validate nnn part of ssn
        var nnnStr = ssn[7..10];
        if (!int.TryParse(nnnStr, out var nnn) || nnn < 2 || nnn > 999)
        {
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
        ChecksumDictionary.TryGetValue(reminder, out var correctChecksum);
     
        if(ssnChecksum != correctChecksum)
        {
            return false;
        }

        return true;
    }
}
