using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;

namespace TypedEnum
{
    [DebuggerStepThrough]
    [DebuggerDisplay("{value}")]
    public struct TypedEnum<T> : IComparable, IComparable<TypedEnum<T>>, IEquatable<TypedEnum<T>>, IEquatable<T>
    where T : struct, Enum
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static readonly Dictionary<T, string> EnumMap;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private T value;

        static TypedEnum()
        {
            var enumValues = (T[])Enum.GetValues(typeof(T));
            EnumMap = new Dictionary<T, string>(enumValues.Length);

            foreach (var enumValue in enumValues)
            {
                EnumMap.Add(enumValue, enumValue.ToString());
            }
        }

        public TypedEnum(T value)
        {
            if (!EnumMap.ContainsKey(value))
            {
                ThrowOnInvalidValue();
            }

            this.value = value;

            // Optimisation. Do not inline.
            static void ThrowOnInvalidValue() => throw new ArgumentException($"Value isn't within the range of defined enum values for {typeof(T)}");
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public T EnumValue // Exposure of inner value for switch statements.
            => this.value;

        public static bool operator ==(TypedEnum<T> left, TypedEnum<T> right)
            => left.Equals(right);

        public static bool operator !=(TypedEnum<T> left, TypedEnum<T> right)
            => !(left == right);

        public static bool operator ==(TypedEnum<T> left, T right)
            => left.Equals(right);

        public static bool operator !=(TypedEnum<T> left, T right)
            => !(left == right);

        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }

            if (obj is TypedEnum<T> te)
            {
                return this.CompareTo(te);
            }

            throw new ArgumentException($"Object must be of type {typeof(T)}");
        }

        public int CompareTo(TypedEnum<T> other)
            => IL.CompareFunc(value, other.value);

        public bool Equals(TypedEnum<T> other)
            => EqualityComparer<T>.Default.Equals(this.value, other.value);

        public bool Equals(T other)
            => EqualityComparer<T>.Default.Equals(this.value, other);

        public static implicit operator T(TypedEnum<T> other)
            => other.value;

        public override bool Equals(object obj)
        {
            if (obj is TypedEnum<T> t)
            {
                return this.Equals(t);
            }
            if (obj is T e)
            {
                return this.Equals(e);
            }

            return false;
        }

        public override int GetHashCode()
            => this.value.GetHashCode();

        public readonly override string ToString()
            => EnumMap[this.value];

        private static class IL
        {
            public static TypeCode EnumTypeCode = Type.GetTypeCode(Enum.GetUnderlyingType(typeof(T)));
            public static Func<T, T, int> CompareFunc { get; } = GetComparer(EnumTypeCode);

            /// <summary>
            /// Creates a ConvertTo function that also converts T to its relevant underlying type.
            /// Avoids boxing for all current valid struct types.
            /// </summary>
            private static Func<T, T, int> GetComparer(TypeCode enumTypeCode)
            {
                var targetType = ResolveType(enumTypeCode);
                var convOpCode = ResolveOpCode(enumTypeCode);
                bool isUnknown = targetType == typeof(object);

                MethodInfo compareMethod = isUnknown
                    ? typeof(T).GetMethod("CompareTo", 0, new[] { typeof(object) })
                    : targetType.GetMethod("CompareTo", 0, new[] { targetType });

                var method = new DynamicMethod(string.Empty, typeof(int), new[] { typeof(T), typeof(T) }, typeof(TypedEnum<T>).Module);
                var il = method.GetILGenerator();

                il.DeclareLocal(targetType);
                il.DeclareLocal(targetType);
                il.DeclareLocal(typeof(int));

                // Convert first parameter to target.
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(convOpCode);
                il.Emit(OpCodes.Stloc_0);

                // Convert second parameter to target
                il.Emit(OpCodes.Ldarg_1);
                il.Emit(convOpCode);
                il.Emit(OpCodes.Stloc_1);

                // Call compare
                il.Emit(OpCodes.Ldloca_S, 0); // <
                il.Emit(OpCodes.Ldloc_1);

                // Handling for unknown types (calls boxed CompareTo method)
                if (isUnknown)
                {
                    il.Emit(OpCodes.Box, typeof(T));
                    il.Emit(OpCodes.Constrained, typeof(T));
                    il.Emit(OpCodes.Callvirt, compareMethod);
                }
                else
                {
                    il.Emit(OpCodes.Call, compareMethod);
                }

                il.Emit(OpCodes.Ret);

                return method.CreateDelegate<Func<T, T, int>>();
            }

            private static Type ResolveType(TypeCode typeCode)
            {
                return typeCode switch
                {
                    TypeCode.SByte => typeof(sbyte),
                    TypeCode.Byte => typeof(byte),
                    TypeCode.Int16 => typeof(short),
                    TypeCode.UInt16 => typeof(ushort),
                    TypeCode.Int32 => typeof(int),
                    TypeCode.UInt32 => typeof(uint),
                    TypeCode.Int64 => typeof(long),
                    TypeCode.UInt64 => typeof(ulong),
                    _ => typeof(object)
                };
            }

            private static OpCode ResolveOpCode(TypeCode typeCode)
            {
                return typeCode switch
                {
                    TypeCode.SByte => OpCodes.Conv_I1,
                    TypeCode.Byte => OpCodes.Conv_U1,
                    TypeCode.Int16 => OpCodes.Conv_I2,
                    TypeCode.UInt16 => OpCodes.Conv_U2,
                    TypeCode.Int32 => OpCodes.Conv_I4,
                    TypeCode.UInt32 => OpCodes.Conv_U4,
                    TypeCode.Int64 => OpCodes.Conv_I8,
                    TypeCode.UInt64 => OpCodes.Conv_U8,
                    _ => OpCodes.Nop
                };
            }
        }
    }
}
