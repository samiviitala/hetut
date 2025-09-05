using System.Diagnostics.CodeAnalysis;

namespace YTunnus;

public class BusinessIdValidator : IBusinessIdValidator
{
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
    
        // Validate base number (positions 0-6) - all must be digits
        for (int i = 0; i < 7; i++)
        {
            if (businessId[i] < '0' || businessId[i] > '9')
                return false;
        }
    
        // Validate check digit (position 8) - must be digit
        char checkChar = businessId[8];
        if (checkChar < '0' || checkChar > '9')
            return false;
    
        int checkDigit = checkChar - '0';
    
        // Calculate expected check digit
        int expectedCheckDigit = CalculateCheckDigit(businessId);
        if (expectedCheckDigit == -1) // Remainder 1, no ID assigned
            return false;
    
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