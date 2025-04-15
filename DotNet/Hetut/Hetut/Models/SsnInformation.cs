namespace Hetut;

/// <summary>
/// Information contained by the SSN
/// </summary>
/// <param name="DateOfBirth">Birth date of the person</param>
/// <param name="Gender">Gender of the person</param>
/// <param name="IsTestSsn">True if the SSN is for testing purposes only, not a real SSN. That is - if the NNN part of the SSN is between 900-999</param>
public record SsnInformation(DateOnly DateOfBirth, Gender Gender, bool IsTestSsn);