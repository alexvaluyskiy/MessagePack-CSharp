extern alias oldmsgpack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using newmsgpack::MessagePack;
using oldmsgpack::MessagePack;

namespace PerfBenchmarkDotNet
{
    public class MyConfig : ManualConfig
    {
        public MyConfig()
        {
            Add(Job.Default.With(Runtime.Clr).With(Jit.RyuJit).With(Platform.X64).WithId("NET4.7_RyuJIT-x64").WithGcServer(true));
        }
    }

    [Config(typeof(MyConfig))]
    [MemoryDiagnoser]
    [BenchmarkCategory("actor")]
    public class ApiBenchmarks
    {
        [Benchmark]
        public ComplexType ActorPath_Parse_local_address()
        {
            MessagePackSerializer.
            return ActorPath.Parse("akka://Sys/user/foo");
        }
    }

    public class ComplexType
    {
        public ComplexType(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public string Name { get; }

        public int Age { get; }
    }
}
