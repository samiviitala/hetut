namespace Hetut.Test.TestCases;

public static class SsnTestCases
{
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

    public static IEnumerable<object> InvalidSsnTestCases
    {
        get
        {
            foreach(var i in TooShortInvalidSsnTestCases) yield return i;
            foreach(var i in TooLongInvalidSsnTestCases) yield return i;
            foreach(var i in WrongChecksumInvalidSsnTestCases) yield return i;
            foreach(var i in WrongNNNPartInvalidSsnTestCases) yield return i;
            foreach(var i in InvalidDateInvalidSsnTestCases) yield return i;
        }
    }

    public static IEnumerable<object> ValidSsnTestCases
    {
        get
        {
            // These test cases cover all combinations of century characters, genders and real/test ssn
            
            // 1800 century
            yield return "140146+9799"; // male, test ssn
            yield return "280788+595L"; // male, real ssn
            yield return "260426+956J"; // female, test ssn
            yield return "011187+6265"; // female, real ssn
            
            // 1900 century, century chars  -, Y, X, W, V, U
            // male, test ssn
            yield return "250768-9255"; 
            yield return "250768Y9255";
            yield return "250768X9255";
            yield return "250768W9255";
            yield return "250768V9255";
            yield return "250768U9255";
            
            // male, real ssn
            yield return "150826-375X";
            yield return "150826Y375X";
            yield return "150826X375X";
            yield return "150826W375X";
            yield return "150826V375X";
            yield return "150826U375X";
            
            // female, test ssn
            yield return "110638-956K";
            yield return "110638Y956K";
            yield return "110638X956K";
            yield return "110638W956K";
            yield return "110638V956K";
            yield return "110638U956K";
            
            // female, real ssn
            yield return "210674-402E"; 
            yield return "210674Y402E";
            yield return "210674X402E";
            yield return "210674W402E";
            yield return "210674V402E";
            yield return "210674U402E";
            
            // 2000 century, century chars A, B, C, D, E, F
            
            // male, test ssn
            yield return "051221A949X";
            yield return "051221B949X";
            yield return "051221C949X";
            yield return "051221D949X";
            yield return "051221E949X";
            yield return "051221F949X";
            
            // male, real ssn
            yield return "031206A299T";
            yield return "031206B299T";
            yield return "031206C299T";
            yield return "031206D299T";
            yield return "031206E299T";
            yield return "031206F299T";
            
            // female, test ssn
            yield return "180110A924T";
            yield return "180110B924T";
            yield return "180110C924T";
            yield return "180110D924T";
            yield return "180110E924T";
            yield return "180110F924T";
            
            // female, real ssn
            yield return "291212A808J";
            yield return "291212B808J";
            yield return "291212C808J";
            yield return "291212D808J";
            yield return "291212E808J";
            yield return "291212F808J";
        }
    }
}