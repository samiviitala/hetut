using BenchmarkDotNet.Attributes;

namespace Hetut.Benchmark;

[MemoryDiagnoser]
public class SsnBenchmark
{
    
    private readonly SsnValidator _ssnValidator = new();
    private readonly SsnGenerator _ssnGenerator = new();
    private readonly SsnExtractor _ssnExtractor = new();

    [Benchmark]
    public bool ValidateValid() => _ssnValidator.Validate("130473-910T");
    
    [Benchmark]
    public bool ValidateInvalidChecksum() => _ssnValidator.Validate("130473-9101");
    
    [Benchmark]
    public string Generate() => _ssnGenerator.GenerateSsn();
    
    [Benchmark]
    public bool ExtractValid() => _ssnExtractor.TryExtract("130473-910T", out _);
    
    [Benchmark]
    public bool ExtractInvalidChecksum() => _ssnExtractor.TryExtract("130473-9101", out _);
}