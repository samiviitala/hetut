namespace Hetut;

/// <inheritdoc /> 
public class SsnGenerator(int? seed = null) : ISsnGenerator
{
    private readonly Random _rng = new(seed ?? new Random().Next());

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
    public string GenerateSsn()
    {
        // Generate random date of birth
        var currentYear = DateTime.Now.Year;
        var year = _rng.Next(1800, currentYear + 1);
        var month = _rng.Next(1, 13);
        var daysInMonth = DateTime.DaysInMonth(year, month);
        var day = _rng.Next(1, daysInMonth + 1);
        
        // Generate the century character based on the year
        var yearReminder = year % 100;
        var century = year - yearReminder;
        var centuryChar = century switch
        {
            2000 => "ABCDEF"[_rng.Next(6)],
            1900 => "YXWVU-"[_rng.Next(6)],
            1800 => '+',
            _ => throw new InvalidOperationException($"SSN generation encountered invalid random generated date with year {year}." +
                                                     $"Century character can only be calculated for following centuries: 1800, 1900, or 2000." +
                                                     $"This error can occur due to system clock being set to future date, since upper boundary of the random date generation is set to current date.")
        };

        // Half male, half female
        // TODO: Validate even distribution of NNN values in unit tests
        int nnn;
        if (_rng.Next(0, 2) == 0)
        {
            // Random odd between 002 - 999
            nnn = _rng.Next(1, 500) * 2 + 1;
        }
        else
        {
            // Random even between 002 - 999
            nnn = _rng.Next(1, 500) * 2;
        }

        var ppkkvvnnn = day * 10000000 + 
                        month * 100000 + 
                        yearReminder * 1000 + 
                        nnn;

        // Calculate the checksum
        var reminder = ppkkvvnnn % 31;
        var checksum = ChecksumChars[reminder];

        return $"{day:D2}{month:D2}{yearReminder:D2}{centuryChar}{nnn:D3}{checksum.ToString()}";
    }
}