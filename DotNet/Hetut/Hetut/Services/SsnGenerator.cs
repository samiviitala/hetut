namespace Hetut;

/// <inheritdoc /> 
public class SsnGenerator : ISsnGenerator
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
    
    /// <inheritdoc />
    public string GenerateSsn(int? seed = null)
    {
        seed ??= new Random().Next();
        var rng = new Random(seed.Value);

        // Generate random date of birth
        // TODO: Add possibility to pass the current date/upper boundary as a parameter
        var currentYear = DateTime.Now.Year;
        var year = rng.Next(1800, currentYear + 1);
        var month = rng.Next(1, 13);
        var daysInMonth = DateTime.DaysInMonth(year, month);
        var day = rng.Next(1, daysInMonth + 1);
        
        // Generate the century character based on the year
        var yearReminder = year % 100;
        var century = year - yearReminder;
        var centuryChar = century switch
        {
            2000 => "ABCDEF"[rng.Next(6)],
            1900 => "YXWVU-"[rng.Next(6)],
            1800 => '+',
            _ => throw new InvalidOperationException($"SSN generation encountered invalid random generated date with year {year}." +
                                                     $"Century character can only be calculated for following centuries: 1800, 1900, or 2000." +
                                                     $"This error can occur due to system clock being set to future date, since upper boundary of the random date generation is set to current date.")
        };

        // Half male, half female
        // TODO: Validate even distribution of NNN values in unit tests
        int nnn;
        if (rng.Next(0, 2) == 0)
        {
            // Random odd between 002 - 999
            nnn = rng.Next(1, 500) * 2 + 1;
        }
        else
        {
            // Random even between 002 - 999
            nnn = rng.Next(1, 500) * 2;
        }

        var ppkkvvnnn = day * 10000000 + 
                        month * 100000 + 
                        yearReminder * 1000 + 
                        nnn;

        // Calculate and validate the checksum
        var reminder = ppkkvvnnn % 31;
        ChecksumDictionary.TryGetValue(reminder, out var correctChecksum);

        return $"{day:D2}{month:D2}{yearReminder:D2}{centuryChar}{nnn:D3}{correctChecksum.ToString()}";
    }
}