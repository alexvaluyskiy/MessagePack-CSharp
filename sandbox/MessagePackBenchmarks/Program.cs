using System;
using BenchmarkDotNet.Running;
using PerfBenchmarkDotNet;

namespace MessagePackBenchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<ResolversDeserializeBenchmarks>();
            BenchmarkRunner.Run<ResolversSerializeBenchmarks>();
            BenchmarkRunner.Run<ApiBenchmarks>();

            Console.ReadLine();
        }
    }
}