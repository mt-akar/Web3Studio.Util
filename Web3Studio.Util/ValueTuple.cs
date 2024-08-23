using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Web3Studio.Util
{
    [Serializable]
    [StructLayout(LayoutKind.Auto)]
    public struct ValueTuple<T1, T2, T3, T4, T5, T6, T7, T8>
        : IEquatable<ValueTuple<T1, T2, T3, T4, T5, T6, T7, T8>>, IStructuralEquatable, IStructuralComparable, IComparable, IComparable<ValueTuple<T1, T2, T3, T4, T5, T6, T7, T8>>, ITuple
    {
        /// <summary>
        /// The current <see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7, T8}"/> instance's first component.
        /// </summary>
        public T1 Item1;

        /// <summary>
        /// The current <see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7, T8}"/> instance's second component.
        /// </summary>
        public T2 Item2;

        /// <summary>
        /// The current <see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7, T8}"/> instance's third component.
        /// </summary>
        public T3 Item3;

        /// <summary>
        /// The current <see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7, T8}"/> instance's fourth component.
        /// </summary>
        public T4 Item4;

        /// <summary>
        /// The current <see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7, T8}"/> instance's fifth component.
        /// </summary>
        public T5 Item5;

        /// <summary>
        /// The current <see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7, T8}"/> instance's sixth component.
        /// </summary>
        public T6 Item6;

        /// <summary>
        /// The current <see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7, T8}"/> instance's seventh component.
        /// </summary>
        public T7 Item7;

        /// <summary>
        /// The current <see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7, T8}"/> instance's eighth component.
        /// </summary>
        public T8 Item8;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7, T8}"/> value type.
        /// </summary>
        /// <param name="item1">The value of the tuple's first component.</param>
        /// <param name="item2">The value of the tuple's second component.</param>
        /// <param name="item3">The value of the tuple's third component.</param>
        /// <param name="item4">The value of the tuple's fourth component.</param>
        /// <param name="item5">The value of the tuple's fifth component.</param>
        /// <param name="item6">The value of the tuple's sixth component.</param>
        /// <param name="item7">The value of the tuple's seventh component.</param>
        /// <param name="item8">The value of the tuple's eighth component.</param>
        public ValueTuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8)
        {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
            Item4 = item4;
            Item5 = item5;
            Item6 = item6;
            Item7 = item7;
            Item8 = item8;
        }

        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            return obj is ValueTuple<T1, T2, T3, T4, T5, T6, T7, T8> tuple && Equals(tuple);
        }

        public bool Equals(ValueTuple<T1, T2, T3, T4, T5, T6, T7, T8> other)
        {
            return EqualityComparer<T1>.Default.Equals(Item1, other.Item1)
                   && EqualityComparer<T2>.Default.Equals(Item2, other.Item2)
                   && EqualityComparer<T3>.Default.Equals(Item3, other.Item3)
                   && EqualityComparer<T4>.Default.Equals(Item4, other.Item4)
                   && EqualityComparer<T5>.Default.Equals(Item5, other.Item5)
                   && EqualityComparer<T6>.Default.Equals(Item6, other.Item6)
                   && EqualityComparer<T7>.Default.Equals(Item7, other.Item7)
                   && EqualityComparer<T8>.Default.Equals(Item8, other.Item8);
        }

        bool IStructuralEquatable.Equals(object? other, IEqualityComparer comparer) =>
            other is ValueTuple<T1, T2, T3, T4, T5, T6, T7, T8> vt &&
            comparer.Equals(Item1, vt.Item1) &&
            comparer.Equals(Item2, vt.Item2) &&
            comparer.Equals(Item3, vt.Item3) &&
            comparer.Equals(Item5, vt.Item5) &&
            comparer.Equals(Item6, vt.Item6) &&
            comparer.Equals(Item7, vt.Item7) &&
            comparer.Equals(Item8, vt.Item8);

        int IComparable.CompareTo(object? other)
        {
            if (other != null)
            {
                if (other is ValueTuple<T1, T2, T3, T4, T5, T6, T7, T8> objTuple)
                {
                    return CompareTo(objTuple);
                }

                throw new ArgumentException("ValueTupleIncorrectType", nameof(other));
            }

            return 1;
        }

        public int CompareTo(ValueTuple<T1, T2, T3, T4, T5, T6, T7, T8> other)
        {
            int c = Comparer<T1>.Default.Compare(Item1, other.Item1);
            if (c != 0) return c;

            c = Comparer<T2>.Default.Compare(Item2, other.Item2);
            if (c != 0) return c;

            c = Comparer<T3>.Default.Compare(Item3, other.Item3);
            if (c != 0) return c;

            c = Comparer<T4>.Default.Compare(Item4, other.Item4);
            if (c != 0) return c;

            c = Comparer<T5>.Default.Compare(Item5, other.Item5);
            if (c != 0) return c;

            c = Comparer<T6>.Default.Compare(Item6, other.Item6);
            if (c != 0) return c;

            c = Comparer<T7>.Default.Compare(Item7, other.Item7);
            if (c != 0) return c;

            return Comparer<T8>.Default.Compare(Item8, other.Item8);
        }

        int IStructuralComparable.CompareTo(object? other, IComparer comparer)
        {
            if (other != null)
            {
                if (other is ValueTuple<T1, T2, T3, T4, T5, T6, T7, T8> objTuple)
                {
                    int c = comparer.Compare(Item1, objTuple.Item1);
                    if (c != 0) return c;

                    c = comparer.Compare(Item2, objTuple.Item2);
                    if (c != 0) return c;

                    c = comparer.Compare(Item3, objTuple.Item3);
                    if (c != 0) return c;

                    c = comparer.Compare(Item4, objTuple.Item4);
                    if (c != 0) return c;

                    c = comparer.Compare(Item5, objTuple.Item5);
                    if (c != 0) return c;

                    c = comparer.Compare(Item6, objTuple.Item6);
                    if (c != 0) return c;

                    c = comparer.Compare(Item7, objTuple.Item7);
                    if (c != 0) return c;

                    return comparer.Compare(Item8, objTuple.Item8);
                }

                throw new ArgumentException("ValueTupleIncorrectType", nameof(other));
            }

            return 1;
        }


        public override int GetHashCode()
        {
            return HashCode.Combine(Item1?.GetHashCode() ?? 0,
                Item2?.GetHashCode() ?? 0,
                Item3?.GetHashCode() ?? 0,
                Item4?.GetHashCode() ?? 0,
                Item5?.GetHashCode() ?? 0,
                Item6?.GetHashCode() ?? 0,
                Item7?.GetHashCode() ?? 0,
                Item8?.GetHashCode() ?? 0);
        }

        int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
        {
            return GetHashCodeCore(comparer);
        }

        private int GetHashCodeCore(IEqualityComparer comparer)
        {
            return HashCode.Combine(comparer.GetHashCode(Item1!),
                comparer.GetHashCode(Item2!),
                comparer.GetHashCode(Item3!),
                comparer.GetHashCode(Item4!),
                comparer.GetHashCode(Item5!),
                comparer.GetHashCode(Item6!),
                comparer.GetHashCode(Item7!),
                comparer.GetHashCode(Item8!));
        }

        public override string ToString()
        {
            return "(" + Item1?.ToString() + ", " + Item2?.ToString() + ", " + Item3?.ToString() + ", " + Item4?.ToString() + ", " + Item5?.ToString() + ", " + Item6?.ToString() + ", " +
                   Item7?.ToString() + ", " + Item8?.ToString() + ")";
        }

        public int Length => 9;

        object? ITuple.this[int index] =>
            index switch
            {
                0 => Item1,
                1 => Item2,
                2 => Item3,
                3 => Item4,
                4 => Item5,
                5 => Item6,
                6 => Item7,
                7 => Item8,
                _ => throw new IndexOutOfRangeException(),
            };
    }

    [Serializable]
    [StructLayout(LayoutKind.Auto)]
    public struct ValueTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9>
        : IEquatable<ValueTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9>>, IStructuralEquatable, IStructuralComparable, IComparable, IComparable<ValueTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9>>, ITuple
    {
        /// <summary>
        /// The current <see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7, T8, T9}"/> instance's first component.
        /// </summary>
        public T1 Item1;

        /// <summary>
        /// The current <see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7, T8, T9}"/> instance's second component.
        /// </summary>
        public T2 Item2;

        /// <summary>
        /// The current <see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7, T8, T9}"/> instance's third component.
        /// </summary>
        public T3 Item3;

        /// <summary>
        /// The current <see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7, T8, T9}"/> instance's fourth component.
        /// </summary>
        public T4 Item4;

        /// <summary>
        /// The current <see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7, T8, T9}"/> instance's fifth component.
        /// </summary>
        public T5 Item5;

        /// <summary>
        /// The current <see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7, T8, T9}"/> instance's sixth component.
        /// </summary>
        public T6 Item6;

        /// <summary>
        /// The current <see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7, T8, T9}"/> instance's seventh component.
        /// </summary>
        public T7 Item7;

        /// <summary>
        /// The current <see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7, T8, T9}"/> instance's eighth component.
        /// </summary>
        public T8 Item8;

        /// <summary>
        /// The current <see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7, T8, T9}"/> instance's ninth component.
        /// </summary>
        public T9 Item9;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValueTuple{T1, T2, T3, T4, T5, T6, T7, T8}"/> value type.
        /// </summary>
        /// <param name="item1">The value of the tuple's first component.</param>
        /// <param name="item2">The value of the tuple's second component.</param>
        /// <param name="item3">The value of the tuple's third component.</param>
        /// <param name="item4">The value of the tuple's fourth component.</param>
        /// <param name="item5">The value of the tuple's fifth component.</param>
        /// <param name="item6">The value of the tuple's sixth component.</param>
        /// <param name="item7">The value of the tuple's seventh component.</param>
        /// <param name="item8">The value of the tuple's eighth component.</param>
        /// <param name="item9">The value of the tuple's ninth component.</param>
        public ValueTuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9)
        {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
            Item4 = item4;
            Item5 = item5;
            Item6 = item6;
            Item7 = item7;
            Item8 = item8;
            Item9 = item9;
        }

        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            return obj is ValueTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9> tuple && Equals(tuple);
        }

        public bool Equals(ValueTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9> other)
        {
            return EqualityComparer<T1>.Default.Equals(Item1, other.Item1)
                   && EqualityComparer<T2>.Default.Equals(Item2, other.Item2)
                   && EqualityComparer<T3>.Default.Equals(Item3, other.Item3)
                   && EqualityComparer<T4>.Default.Equals(Item4, other.Item4)
                   && EqualityComparer<T5>.Default.Equals(Item5, other.Item5)
                   && EqualityComparer<T6>.Default.Equals(Item6, other.Item6)
                   && EqualityComparer<T7>.Default.Equals(Item7, other.Item7)
                   && EqualityComparer<T8>.Default.Equals(Item8, other.Item8)
                   && EqualityComparer<T9>.Default.Equals(Item9, other.Item9);
        }

        bool IStructuralEquatable.Equals(object? other, IEqualityComparer comparer) =>
            other is ValueTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9> vt &&
            comparer.Equals(Item1, vt.Item1) &&
            comparer.Equals(Item2, vt.Item2) &&
            comparer.Equals(Item3, vt.Item3) &&
            comparer.Equals(Item5, vt.Item5) &&
            comparer.Equals(Item6, vt.Item6) &&
            comparer.Equals(Item7, vt.Item7) &&
            comparer.Equals(Item8, vt.Item8) &&
            comparer.Equals(Item9, vt.Item9);

        int IComparable.CompareTo(object? other)
        {
            if (other != null)
            {
                if (other is ValueTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9> objTuple)
                {
                    return CompareTo(objTuple);
                }

                throw new ArgumentException("ValueTupleIncorrectType", nameof(other));
            }

            return 1;
        }

        public int CompareTo(ValueTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9> other)
        {
            int c = Comparer<T1>.Default.Compare(Item1, other.Item1);
            if (c != 0) return c;

            c = Comparer<T2>.Default.Compare(Item2, other.Item2);
            if (c != 0) return c;

            c = Comparer<T3>.Default.Compare(Item3, other.Item3);
            if (c != 0) return c;

            c = Comparer<T4>.Default.Compare(Item4, other.Item4);
            if (c != 0) return c;

            c = Comparer<T5>.Default.Compare(Item5, other.Item5);
            if (c != 0) return c;

            c = Comparer<T6>.Default.Compare(Item6, other.Item6);
            if (c != 0) return c;

            c = Comparer<T7>.Default.Compare(Item7, other.Item7);
            if (c != 0) return c;

            c = Comparer<T8>.Default.Compare(Item8, other.Item8);
            if (c != 0) return c;

            return Comparer<T9>.Default.Compare(Item9, other.Item9);
        }

        int IStructuralComparable.CompareTo(object? other, IComparer comparer)
        {
            if (other != null)
            {
                if (other is ValueTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9> objTuple)
                {
                    int c = comparer.Compare(Item1, objTuple.Item1);
                    if (c != 0) return c;

                    c = comparer.Compare(Item2, objTuple.Item2);
                    if (c != 0) return c;

                    c = comparer.Compare(Item3, objTuple.Item3);
                    if (c != 0) return c;

                    c = comparer.Compare(Item4, objTuple.Item4);
                    if (c != 0) return c;

                    c = comparer.Compare(Item5, objTuple.Item5);
                    if (c != 0) return c;

                    c = comparer.Compare(Item6, objTuple.Item6);
                    if (c != 0) return c;

                    c = comparer.Compare(Item7, objTuple.Item7);
                    if (c != 0) return c;

                    c = comparer.Compare(Item8, objTuple.Item8);
                    if (c != 0) return c;

                    return comparer.Compare(Item9, objTuple.Item9);
                }

                throw new ArgumentException("ValueTupleIncorrectType", nameof(other));
            }

            return 1;
        }


        public override int GetHashCode()
        {
            var hash0 = HashCode.Combine(Item1?.GetHashCode() ?? 0,
                Item2?.GetHashCode() ?? 0,
                Item3?.GetHashCode() ?? 0,
                Item4?.GetHashCode() ?? 0,
                Item5?.GetHashCode() ?? 0,
                Item6?.GetHashCode() ?? 0,
                Item7?.GetHashCode() ?? 0,
                Item8?.GetHashCode() ?? 0);
            var hash1 = Item9?.GetHashCode() ?? 0;
            return HashCode.Combine(hash0, hash1);
        }

        int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
        {
            return GetHashCodeCore(comparer);
        }

        private int GetHashCodeCore(IEqualityComparer comparer)
        {
            var hash0 = HashCode.Combine(comparer.GetHashCode(Item1!),
                comparer.GetHashCode(Item2!),
                comparer.GetHashCode(Item3!),
                comparer.GetHashCode(Item4!),
                comparer.GetHashCode(Item5!),
                comparer.GetHashCode(Item6!),
                comparer.GetHashCode(Item7!),
                comparer.GetHashCode(Item8!));
            var hash1 = comparer.GetHashCode(Item9!);
            return HashCode.Combine(hash0, hash1);
        }

        public override string ToString()
        {
            return "(" + Item1?.ToString() + ", " + Item2?.ToString() + ", " + Item3?.ToString() + ", " + Item4?.ToString() + ", " + Item5?.ToString() + ", " + Item6?.ToString() + ", " +
                   Item7?.ToString() + ", " + Item8?.ToString() + ", " + Item9?.ToString() + ")";
        }

        public int Length => 9;

        object? ITuple.this[int index] =>
            index switch
            {
                0 => Item1,
                1 => Item2,
                2 => Item3,
                3 => Item4,
                4 => Item5,
                5 => Item6,
                6 => Item7,
                7 => Item8,
                8 => Item9,
                _ => throw new IndexOutOfRangeException(),
            };
    }
}