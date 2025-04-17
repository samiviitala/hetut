namespace Hetut;

/// <summary>
/// Information contained by the SSN
/// </summary>
/// <param name="DateOfBirth">Birth date of the person</param>
/// <param name="Gender">Gender of the person</param>
/// <param name="IsTestSsn">True if the SSN is for testing purposes only, not a real SSN. That is - if the NNN part of the SSN is between 900-999</param>
/// <param name="IsAfter2023Reform">True if century character is one of those which are only used after 2023 SSN reform (<a href="https://dvv.fi/hetu-uudistus">Henkilötunnuksen välimerkkiuudistus</a>)  </param>
public record SsnInformation(DateOnly DateOfBirth, Gender Gender, bool IsTestSsn, bool IsAfter2023Reform);