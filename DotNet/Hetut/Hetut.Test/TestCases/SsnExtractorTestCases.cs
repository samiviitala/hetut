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
}