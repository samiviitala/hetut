using System.Diagnostics.CodeAnalysis;

namespace YTunnus;

public class BusinessIdValidator : IBusinessIdValidator
{
    public bool Validate([NotNullWhen(true)] string? businessId)
    {
        if (string.IsNullOrWhiteSpace(businessId))
            return false;
        
        
        // Reject input with leading or trailing whitespace
        if (businessId != businessId.Trim())
            return false;
    
        // Parse business ID format - must contain dash
        if (!businessId.Contains('-'))
            return false;
    
        var parts = businessId.Split('-');
        if (parts.Length != 2 || parts[1].Length != 1)
            return false;
    
        string baseNumber = parts[0];
        if (!int.TryParse(parts[1], out int checkDigit))
            return false;
    
        // Validate base number
        if (baseNumber.Length < 6 || baseNumber.Length > 7 || !baseNumber.All(char.IsDigit))
            return false;
    
        // Calculate correct check digit
        int expectedCheckDigit = CalculateCheckDigit(baseNumber);
        if (expectedCheckDigit == -1) // Remainder 1, no ID assigned
            return false;
    
        // Validate the provided check digit
        return checkDigit == expectedCheckDigit;
    }

    private static int CalculateCheckDigit(string baseNumber)
    {
        // Täydennä 7 numeroksi (lisää etunollia vasemmalle tarvittaessa)
        baseNumber = baseNumber.PadLeft(7, '0');

        // Kertoimet vasemmalta oikealle
        int[] multipliers = { 7, 9, 10, 5, 8, 4, 2 };
        var sum = 0;

        for (var i = 0; i < 7; i++)
        {
            var digit = int.Parse(baseNumber[i].ToString());
            sum += digit * multipliers[i];
        }

        var remainder = sum % 11;

        return remainder switch
        {
            0 => 0,
            1 => -1, // Tunnusta ei anneta
            _ => 11 - remainder
        };
    }
}