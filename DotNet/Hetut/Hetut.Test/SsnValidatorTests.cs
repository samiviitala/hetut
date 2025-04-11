namespace Hetut.Test;

public class Tests
{
   
    [TestCaseSource(nameof(ValidSsnTestCases))]
    public void Validate_GivenValidSsn_ReturnsTrue(string ssn)
    {
        var sut = new SsnValidator();
        var result = sut.Validate(ssn);
        Assert.That(result, Is.True, $"Expected true for valid SSN: {ssn}");
    }
    
    [TestCaseSource(nameof(TooShortInvalidSsnTestCases))]
    [TestCaseSource(nameof(TooLongInvalidSsnTestCases))]
    [TestCaseSource(nameof(WrongChecksumInvalidSsnTestCases))]
    [TestCaseSource(nameof(WrongNNNPartInvalidSsnTestCases))]
    [TestCaseSource(nameof(InvalidDateInvalidSsnTestCases))]
    public void Validate_GivenInvalidSsn_ReturnsFalse(string ssn)
    {
        var sut = new SsnValidator();
        var result = sut.Validate(ssn);
        Assert.That(result, Is.False, $"Expected false for invalid SSN: {ssn}");
    }
    
    public static IEnumerable<object> TooShortInvalidSsnTestCases
    {
        get
        {
            yield return "0";
            yield return "0105";
            yield return "020594X903";
        }
    }
    
    public static IEnumerable<object> TooLongInvalidSsnTestCases
    {
        get
        {
            yield return "010594Y90322";
            yield return "010594Y90322010594Y90322";
        }
    }

    public static IEnumerable<object> WrongChecksumInvalidSsnTestCases
    {
        get
        {
            yield return "010594Y9031";
            yield return "010594Y9022";
            yield return "020594X9033";
        }
    }
    
    public static IEnumerable<object> WrongNNNPartInvalidSsnTestCases
    {
        get
        {
            // These cases have valid checksum but invalid NNN value
            yield return "010594Y0011";
        }
    }
    
    public static IEnumerable<object> InvalidDateInvalidSsnTestCases
    {
        get
        {
            // February does not have 30 days
            yield return "300201A903B";
        }
    }

    public static IEnumerable<object> ValidSsnTestCases
    {
        get
        {
            yield return "010594Y9032";
            yield return "010594Y9021";
            yield return "020594X903P";
            yield return "020594X902N";
            yield return "030594W903B";
            yield return "030694W9024";
            yield return "040594V9030";
            yield return "040594V902Y";
            yield return "050594U903M";
            yield return "050594U902L";
            yield return "010516B903X";
            yield return "010516B902W";
            yield return "020516C903K";
            yield return "020516C902J";
            yield return "030516D9037";
            yield return "030516D9026";
            yield return "010501E9032";
            yield return "020502E902X";
            yield return "020503F9037";
            yield return "020504A902E";
            yield return "020504B904H";
        }
    }
}