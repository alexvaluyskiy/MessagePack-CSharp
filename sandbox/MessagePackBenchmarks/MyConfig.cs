using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Toolchains.CsProj;

namespace PerfBenchmarkDotNet
{
    public class MyConfig : ManualConfig
    {
        public MyConfig()
        {
            Add(Job.Default.With(Runtime.Clr).With(Jit.RyuJit).With(Platform.X64).WithGcServer(true).WithId("NET 4.7"));
            Add(Job.Default.With(Runtime.Core).With(Platform.X64).With(CsProjCoreToolchain.NetCoreApp11).WithGcServer(true).WithId("NETCore 1.1"));
        }
    }
}
