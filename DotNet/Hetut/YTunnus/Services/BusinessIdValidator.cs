using System.Diagnostics.CodeAnalysis;
// ReSharper disable IdentifierTypo

namespace YTunnus;

/// <inheritdoc /> 
public class BusinessIdValidator : IBusinessIdValidator
{
    
    /// <summary>
    ///     Checksum multipliers
    /// </summary>
    private readonly int[] _multipliers = { 7, 9, 10, 5, 8, 4, 2 };
    
    /// <inheritdoc />
    public bool Validate([NotNullWhen(true)] string? businessId)
    {
        if (businessId == null)
            return false;
    
        // Valid business ID is always exactly 9 characters: nnnnnnn-t
        if (businessId.Length != 9)
            return false;
    
        // Dash must be at position 7
        if (businessId[7] != '-')
            return false;
    
        // Validate base number (positions 0-7) - all must be digits
        if (!int.TryParse(businessId[..7], out var nnnnnn))
        {
            return false;
        }
    
        // Validate check digit (position 8) - must be digit
        var checkChar = businessId[8];
        if (checkChar < '0' || checkChar > '9')
            return false;
    
        var checkDigit = checkChar - '0';
    
        // Calculate expected check digit
        var expectedCheckDigit = CalculateChecksum(nnnnnn);
        if (expectedCheckDigit == -1) // Remainder 1, no ID assigned
            return false;
    
        return checkDigit == expectedCheckDigit;
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