using System;
using System.IO;
using BenchmarkDotNet.Attributes;
using MessagePack;

namespace PerfBenchmarkDotNet
{
    [Config(typeof(MyConfig))]
    [MemoryDiagnoser]
    [BenchmarkCategory("actor")]
    public class ApiBenchmarks
    {
        private ComplexType TestObject = new ComplexType("John", 65);
        private byte[] TestBytes = Convert.FromBase64String("kqRKb2huQQ==");

        [Benchmark]
        public byte[] MessagePackSerializer_Serialize_ByteArray()
        {
            return MessagePackSerializer.Serialize(TestObject);
        }

        [Benchmark]
        public ComplexType MessagePackSerializer_Deserialize_ByteArray()
        {
            return MessagePackSerializer.Deserialize<ComplexType>(TestBytes);
        }

        [Benchmark]
        public Span<byte> MessagePackSerializer_Serialize_Span()
        {
            Span<byte> bytes;
            MessagePackSerializer.Serialize(TestObject, out bytes);
            return bytes;
        }

        [Benchmark]
        public ComplexType MessagePackSerializer_Deserialize_Span()
        {
            var buffer = new ReadOnlySpan<byte>(TestBytes);
            return MessagePackSerializer.Deserialize<ComplexType>(buffer);
        }

        [Benchmark]
        public byte[] MessagePackSerializer_Serialize_Stream()
        {
            using (var stream = new MemoryStream())
            {
                MessagePackSerializer.Serialize(stream, TestObject);
                stream.Flush();
                return stream.ToArray();
            }
        }

        [Benchmark]
        public ComplexType MessagePackSerializer_Deserialize_Stream()
        {
            using (var stream = new MemoryStream(TestBytes))
            {
                return MessagePackSerializer.Deserialize<ComplexType>(stream);
            }
        }

        [Benchmark]
        public ArraySegment<byte> MessagePackSerializer_SerializeUnsafe_ArraySegment()
        {
            return MessagePackSerializer.SerializeUnsafe(TestObject);
        }

        [Benchmark]
        public byte[] MessagePackSerializer_NonGeneric_Serialize_ByteArray()
        {
            return MessagePackSerializer.NonGeneric.Serialize(typeof(ComplexType), TestObject);
        }

        [Benchmark]
        public ComplexType MessagePackSerializer_NonGeneric_Deserialize_ByteArray()
        {
            return (ComplexType)MessagePackSerializer.NonGeneric.Deserialize(typeof(ComplexType), TestBytes);
        }
    }

    [MessagePackObject]
    public class ComplexType
    {
        public ComplexType(string name, int age)
        {
            Name = name;
            Age = age;
        }

        [Key(0)]
        public string Name { get; }

        [Key(1)]
        public int Age { get; }
    }
}
