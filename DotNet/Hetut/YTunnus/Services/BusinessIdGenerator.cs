namespace YTunnus;

/// <inheritdoc /> 
public class BusinessIdGenerator : IBusinessIdGenerator
{

    private readonly Random _rng;
    
    /// <summary>
    ///     Checksum multipliers
    /// </summary>
    private readonly int[] _multipliers = { 7, 9, 10, 5, 8, 4, 2 };
    
    public BusinessIdGenerator(BusinessIdGeneratorOptions? options = null)
    {
        options ??= BusinessIdGeneratorOptions.Create();
        _rng = new Random(options.Seed);
    }
    
    /// <inheritdoc /> 
    public string Generate()
    {
        // Generate nnnnnnn part where the value will not start with two zeroes
        // That is, between [0100000, 9999999]
        var nnnnnnn = _rng.Next(0100000, 10_000_000);
        var checksum = CalculateChecksum(nnnnnnn);
        return $"{nnnnnnn}-{checksum}";

    }
    
    private int CalculateChecksum(int baseNumber)
    {
        // Extract individual digits from the integer (rightmost digit first)
        var digits = new int[7];
        var temp = baseNumber;
    
        // Fill from right to left, pad with zeros if needed
        for (var i = 6; i >= 0; i--)
        {
            digits[i] = temp % 10;
            temp /= 10;
        }
    
        var sum = 0;
    
        // Calculate sum using extracted digits
        for (var i = 0; i < 7; i++)
        {
            sum += digits[i] * _multipliers[i];
        }
    
        var remainder = sum % 11;
    
        return remainder switch
        {
            0 => 0,
            1 => -1, // No ID assigned
            _ => 11 - remainder
        };
    }
}