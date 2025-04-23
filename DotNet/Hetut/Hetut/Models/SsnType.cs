namespace Hetut;

/// <summary>
///     Type of social security number
/// </summary>
[Flags]
public enum SsnType
{
    /// <summary>
    ///     Real SSN where NNN part is between 002-899. Note that this kind of SSN might actually belong to a living person, use with caution.
    /// </summary>
    Real,
    /// <summary>
    ///     Test SSN where NNN part is between 900-999
    /// </summary>
    Test
}