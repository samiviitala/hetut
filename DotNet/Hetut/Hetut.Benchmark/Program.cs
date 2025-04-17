using BenchmarkDotNet.Running;

namespace Hetut.Benchmark;

public class Program
{
    public static void Main(string[] args)
    {
        var summary = BenchmarkRunner.Run<SsnValidatorBenchmark>();
    }
}