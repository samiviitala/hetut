using System.Collections;

namespace Hetut.Test.TestCases;

public class SsnExtractorTestCases
{
    public static IEnumerable DateOfBirthTestCases
    {
        get
        {
            // Test SSNs
            yield return new TestCaseData("160300+917A").Returns(new DateOnly(1800, 3, 16));
            yield return new TestCaseData("180735-909L").Returns(new DateOnly(1935, 7, 18));
            yield return new TestCaseData("050115A9341").Returns(new DateOnly(2015, 1, 5));
            
            // Real SSNs
            yield return new TestCaseData("160300+810V").Returns(new DateOnly(1800, 3, 16));
            yield return new TestCaseData("180735-690J").Returns(new DateOnly(1935, 7, 18));
            yield return new TestCaseData("050115A216V").Returns(new DateOnly(2015, 1, 5));
        }
    }
    
    public static IEnumerable GenderTestCases
    {
        get
        {
            // Test SSNs
            yield return new TestCaseData("160300+984F").Returns(Gender.Female);
            yield return new TestCaseData("180735-964C").Returns(Gender.Female);
            yield return new TestCaseData("050115A982K").Returns(Gender.Female);
            yield return new TestCaseData("160300+993S").Returns(Gender.Male);
            yield return new TestCaseData("180735-949W").Returns(Gender.Male);
            yield return new TestCaseData("050115A993X").Returns(Gender.Male);
            
            // Real SSNs
            yield return new TestCaseData("160300+706H").Returns(Gender.Female);
            yield return new TestCaseData("180735-732W").Returns(Gender.Female);
            yield return new TestCaseData("050115A404X").Returns(Gender.Female);
            yield return new TestCaseData("160300+775R").Returns(Gender.Male);
            yield return new TestCaseData("180735-8911").Returns(Gender.Male);
            yield return new TestCaseData("050115A775W").Returns(Gender.Male);
        }
    }
    
    public static IEnumerable IsTestSSNTestCases
    {
        get
        {
            // Test SSNs
            yield return new TestCaseData("160300+984F").Returns(true);
            yield return new TestCaseData("180735-964C").Returns(true);
            yield return new TestCaseData("050115A982K").Returns(true);
            yield return new TestCaseData("160300+993S").Returns(true);
            yield return new TestCaseData("180735-949W").Returns(true);
            yield return new TestCaseData("050115A993X").Returns(true);
            
            // Real SSNs
            yield return new TestCaseData("160300+706H").Returns(false);
            yield return new TestCaseData("180735-732W").Returns(false);
            yield return new TestCaseData("050115A404X").Returns(false);
            yield return new TestCaseData("160300+775R").Returns(false);
            yield return new TestCaseData("180735-8911").Returns(false);
            yield return new TestCaseData("050115A775W").Returns(false);
        }
    }
    
    public static IEnumerable IsAfter2023ReformTestCases
    {
        get
        {
            // Test SSNs after 2023 reform
            yield return new TestCaseData("240175Y955E").Returns(true); // 1900 century, test ssn, Y
            yield return new TestCaseData("050910X919P").Returns(true); // 1900 century, test ssn, X
            yield return new TestCaseData("110343W9431").Returns(true); // 1900 century, test ssn, W
            yield return new TestCaseData("260746V931A").Returns(true); // 1900 century, test ssn, V
            yield return new TestCaseData("020123U975F").Returns(true); // 1900 century, test ssn, U
            
            // Real SSNs after 2023 reform
            yield return new TestCaseData("090280Y643T").Returns(true); // 1900 century, test ssn, Y
            yield return new TestCaseData("130688X665C").Returns(true); // 1900 century, test ssn, X
            yield return new TestCaseData("240379W449S").Returns(true); // 1900 century, test ssn, W
            yield return new TestCaseData("290946V499X").Returns(true); // 1900 century, test ssn, V
            yield return new TestCaseData("070254U337X").Returns(true); // 1900 century, test ssn, U
            
            // Test SSNs before 2023 reform
            yield return new TestCaseData("100201+9853").Returns(false); // 1800 century, test ssn, +
            yield return new TestCaseData("280892-971K").Returns(false); // 1900 century, test ssn, -
            yield return new TestCaseData("130505A981B").Returns(false); // 2000 century, test ssn, A
            
            // Real SSNs before 2023 reform
            yield return new TestCaseData("130338+039V").Returns(false); // 1800 century, test ssn, +
            yield return new TestCaseData("131244-511W").Returns(false); // 1900 century, test ssn, -
            yield return new TestCaseData("200321A3213").Returns(false); // 2000 century, test ssn, A
        }
    }
}