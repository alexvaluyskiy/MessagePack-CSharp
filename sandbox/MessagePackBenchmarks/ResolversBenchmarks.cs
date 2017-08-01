using System;
using BenchmarkDotNet.Attributes;
using MessagePack;
using MessagePack.Resolvers;

namespace PerfBenchmarkDotNet
{
    [Config(typeof(MyConfig))]
    [MemoryDiagnoser]
    public class ResolversSerializeBenchmarks
    {
        private ContractType TestContractType = new ContractType("John", 65);
        private ContractlessType TestContractlessType = new ContractlessType("John", 65);
        private TypelessPrimitiveType TestTypelessPrimitiveType = new TypelessPrimitiveType("http://test.com", 65);
        private TypelessPrimitiveType TestTypelessComplexType = new TypelessPrimitiveType(new Uri("http://test.com"), 65);

        [Benchmark(Baseline = true)]
        public byte[] MessagePackSerializer_Serialize_StandardResolver()
        {
            return MessagePackSerializer.Serialize(TestContractType, StandardResolver.Instance);
        }

        [Benchmark]
        public byte[] MessagePackSerializer_Serialize_ContractlessStandardResolver()
        {
            return MessagePackSerializer.Serialize(TestContractlessType, ContractlessStandardResolver.Instance);
        }

        [Benchmark]
        public byte[] MessagePackSerializer_Serialize_TypelessContractlessStandardResolver_primitive()
        {
            return MessagePackSerializer.Serialize(TestTypelessPrimitiveType, TypelessContractlessStandardResolver.Instance);
        }

        [Benchmark]
        public byte[] MessagePackSerializer_Serialize_TypelessContractlessStandardResolver_complex()
        {
            return MessagePackSerializer.Serialize(TestTypelessComplexType, TypelessContractlessStandardResolver.Instance);
        }
    }

    [Config(typeof(MyConfig))]
    [MemoryDiagnoser]
    public class ResolversDeserializeBenchmarks
    {
        private byte[] StandardResolverBytes = MessagePackSerializer.Serialize(new ContractType("John", 65), StandardResolver.Instance);
        private byte[] ContractlessStandardResolverBytes = MessagePackSerializer.Serialize(new ContractlessType("John", 65), ContractlessStandardResolver.Instance);
        private byte[] TypelessContractlessStandardResolverBytes = MessagePackSerializer.Serialize(new TypelessPrimitiveType("http://test.com", 65), TypelessContractlessStandardResolver.Instance);
        private byte[] TypelessContractlessStandardResolverComplexBytes = MessagePackSerializer.Serialize(new TypelessPrimitiveType(new Uri("http://test.com"), 65), TypelessContractlessStandardResolver.Instance);

        [Benchmark(Baseline = true)]
        public ContractType MessagePackSerializer_Deserialize_StandardResolver()
        {
            return MessagePackSerializer.Deserialize<ContractType>(StandardResolverBytes, StandardResolver.Instance);
        }

        [Benchmark]
        public ContractlessType MessagePackSerializer_Deserialize_ContractlessStandardResolver()
        {
            return MessagePackSerializer.Deserialize<ContractlessType>(ContractlessStandardResolverBytes, ContractlessStandardResolver.Instance);
        }

        [Benchmark]
        public TypelessPrimitiveType MessagePackSerializer_Deserialize_TypelessContractlessStandardResolver()
        {
            return MessagePackSerializer.Deserialize<TypelessPrimitiveType>(TypelessContractlessStandardResolverBytes, TypelessContractlessStandardResolver.Instance);
        }

        [Benchmark]
        public TypelessPrimitiveType MessagePackSerializer_Deserialize_TypelessContractlessStandardResolverComplexBytes()
        {
            return MessagePackSerializer.Deserialize<TypelessPrimitiveType>(TypelessContractlessStandardResolverComplexBytes, TypelessContractlessStandardResolver.Instance);
        }
    }

    [MessagePackObject]
    public class ContractType
    {
        public ContractType(string name, int age)
        {
            Name = name;
            Age = age;
        }

        [Key(0)]
        public string Name { get; }

        [Key(1)]
        public int Age { get; }
    }

    public class ContractlessType
    {
        public ContractlessType(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public string Name { get; }

        public int Age { get; }
    }

    public class TypelessPrimitiveType
    {
        public TypelessPrimitiveType(object name, int age)
        {
            Name = name;
            Age = age;
        }

        public object Name { get; }

        public int Age { get; }
    }
}
