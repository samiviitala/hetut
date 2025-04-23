using BenchmarkDotNet.Attributes;

namespace Hetut.Benchmark;


//| Method                  | Mean      | Error    | StdDev   | Gen0   | Allocated |
//|------------------------ |----------:|---------:|---------:|-------:|----------:|
//| ValidateValid           |  25.22 ns | 0.334 ns | 0.260 ns | 0.0068 |     128 B |
//| ValidateInvalidChecksum |  24.64 ns | 0.245 ns | 0.217 ns | 0.0068 |     128 B |
//| Generate                | 140.94 ns | 0.246 ns | 0.218 ns | 0.0038 |      72 B |
//| ExtractValid            |  28.91 ns | 0.599 ns | 1.196 ns | 0.0085 |     160 B |
//| ExtractInvalidChecksum  |  24.33 ns | 0.393 ns | 0.367 ns | 0.0068 |     128 B |

[MemoryDiagnoser]
public class SsnBenchmark
{
    
    private readonly SsnValidator _ssnValidator = new();
    private readonly SsnGenerator _ssnGenerator = new();
    private readonly SsnExtractor _ssnExtractor = new();

    // [Benchmark]
    // public bool ValidateValid() => _ssnValidator.Validate("130473-910T");
    //
    // [Benchmark]
    // public bool ValidateInvalidChecksum() => _ssnValidator.Validate("130473-9101");
    
    [Benchmark]
    public string Generate() => _ssnGenerator.GenerateSsn();
    
    // [Benchmark]
    // public bool ExtractValid() => _ssnExtractor.TryExtract("130473-910T", out _);
    //
    // [Benchmark]
    // public bool ExtractInvalidChecksum() => _ssnExtractor.TryExtract("130473-9101", out _);
}