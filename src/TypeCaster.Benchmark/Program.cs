using System;
using BenchmarkDotNet.Running;

namespace TypeCaster.Benchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<TypeCastBenchmark>();
        }
    }
}