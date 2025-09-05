using BenchmarkDotNet.Attributes;

namespace YTunnus.Benchmark;

[MemoryDiagnoser]
public class YTunnusBenchmark
{

    private readonly BusinessIdValidator _businessIdValidator = new();

    [Benchmark]
    public bool ValidateValid() => _businessIdValidator.Validate("3280643-9");

    [Benchmark]
    public bool ValidateInvalidChecksum() => _businessIdValidator.Validate("3280643-8");
}