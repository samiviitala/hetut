namespace Hetut;

/// <summary>
///     Options for the SSN generator
/// </summary>
/// <param name="Seed">Seed value for randomness. With same seed, the generator will always generate same sequence of ssns.</param>
/// <param name="IsTestSsn">Do we want to generate real ssns with NNN value between 002-899, or test ssns with NNN value between 900-999?</param>
/// <param name="Genders">Gender for the ssn will be randomly picked from this array. E.g. [Gender.Female, Gender.Male] for even distribution between these two.</param>
/// <param name="IncludeReform2023CenturyCharacters">Include century characters after 2023 reform?</param>
/// <param name="DateOfBirthMin">Minimum date of birth. Must be higher than 01.01.1800. Use same value as <see cref="DateOfBirthMax"/> for fixed date of birth.</param>
/// <param name="DateOfBirthMax">Maximum date of birth. Use same value as <see cref="DateOfBirthMin"/> for fixed date of birth.</param>
public record SsnGeneratorOptions(
    int Seed, 
    bool IsTestSsn, 
    Gender[] Genders, 
    bool IncludeReform2023CenturyCharacters,
    DateOnly? DateOfBirthMin, 
    DateOnly? DateOfBirthMax)
{

    public static SsnGeneratorOptions Create(
        int? seed = null, 
        bool? isTestSsn = null, 
        Gender[]? genders = null, 
        bool? includeReform2023CenturyCharacters = null,
        DateOnly? dateOfBirthMin = null, 
        DateOnly? dateOfBirthMax = null)
    {
        if(dateOfBirthMin < new DateOnly(1800, 1, 1))
            throw new ArgumentOutOfRangeException(nameof(dateOfBirthMin), "Minimum date of birth cannot be before 01.01.1800");
        
        return new SsnGeneratorOptions(
            seed ?? new Random().Next(), 
            isTestSsn ?? true,
            genders ?? new [] { Gender.Female, Gender.Male },
            includeReform2023CenturyCharacters ?? true,
            dateOfBirthMin ?? new DateOnly(1800, 1, 1),
            dateOfBirthMax ?? DateOnly.FromDateTime(DateTime.Now));
    }
}