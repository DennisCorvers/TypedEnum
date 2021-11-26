using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;
using TypedEnum;

namespace Benchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<TypedEnumBenchmarks>();
            Console.ReadLine();
        }
    }

    [MemoryDiagnoser]
    public class TypedEnumBenchmarks
    {
        TypedEnum<TestEnum> value1 = new(TestEnum.BatchModeChange);
        TypedEnum<TestEnum> value2 = new(TestEnum.GotDesignerEventService);

        HashSet<TestEnum> enumSet = new(10);
        HashSet<TypedEnum<TestEnum>> typedEnumSet = new(10);

        [Benchmark]
        public void Constructor()
        {
            var value = TestEnum.BatchModeChange;
        }

        [Benchmark]
        public void EnumAssign()
        {
            var value = new TypedEnum<TestEnum>(TestEnum.BatchModeChange);
        }

        [Benchmark]
        public void NativeCompareTo()
        {
            var value = CompareTo(TestEnum.BatchMode, TestEnum.GotDesignerEventService);

            // Comparing of generic enum values
            static int CompareTo<T>(T left, T right)
                where T : struct, Enum
            {
                return left.CompareTo(right);
            }
        }

        [Benchmark]
        public void ILCompareTo()
        {
            var value = value1.CompareTo(value2);
        }

        [Benchmark]
        public void NativeToString()
        {
            var value = TestEnum.BatchModeChange.ToString();
        }

        [Benchmark]
        public void CachedToString()
        {
            var value = value1.ToString();
        }

        [Benchmark]
        public void HashSetEnum()
        {
            enumSet.Add(TestEnum.ReInitTab);
            enumSet.Contains(TestEnum.SysColorChangeRefresh);
            enumSet.Remove(TestEnum.ReInitTab);
        }

        [Benchmark]
        public void HashSetTypedEnum()
        {
            typedEnumSet.Add(value1);
            typedEnumSet.Contains(value2);
            typedEnumSet.Remove(value1);
        }
    }

    public enum TestEnum
    {
        PropertiesChanged = 0x0001,
        GotDesignerEventService = 0x0002,
        InternalChange = 0x0004,
        TabsChanging = 0x0008,
        BatchMode = 0x0010,
        ReInitTab = 0x0020,
        SysColorChangeRefresh = 0x0040,
        FullRefreshAfterBatch = 0x0080,
        BatchModeChange = 0x0100,
        RefreshingProperties = 0x0200
    }
}
