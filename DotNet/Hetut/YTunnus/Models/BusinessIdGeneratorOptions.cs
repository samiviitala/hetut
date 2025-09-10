namespace YTunnus;

/// <summary>
///     Options for the SSN generator
/// </summary>
/// <param name="Seed">
///     Seed value for randomness. With same seed, the generator will always generate same sequence of
///     business ids.
/// </param>
public record BusinessIdGeneratorOptions(
    int Seed
)
{
    public static BusinessIdGeneratorOptions Create(
        int? seed = null)
    {
        return new BusinessIdGeneratorOptions(
            seed ?? new Random().Next());
    }
}