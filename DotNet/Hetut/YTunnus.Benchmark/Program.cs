using BenchmarkDotNet.Running;

namespace YTunnus.Benchmark;

public class Program
{
    public static void Main(string[] args)
    {
        var summary = BenchmarkRunner.Run<YTunnusBenchmark>();
    }
}