using System;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using Nethereum.Util;

namespace Web3Studio.Util
{
    /// <summary>
    /// An immutable abstract number type that can hold arbitrarily big non-negative integer numbers.
    /// Hex bridges IO of the Ethereum APIs with C# types.
    /// The hex representation is a big endian hex string with small letters and "0x" prefix, which Ethereum APIs accept and return.
    /// 
    /// If the hex is constructed through the <see cref="Integer"/> constructor, it doesn't have any leading zeros in <see cref="HexString"/> and <see cref="Bytes"/>.
    /// If the hex is constructed with leading zeros through <see cref="HexString"/> or <see cref="Bytes"/> constructors, those zeros will not be erased and be kept in <see cref="HexString"/> and <see cref="Bytes"/>.
    /// This doesn't affect the numerical value of the <see cref="Hex"/>.
    /// 
    /// The decimal representation uses the .NET's native <see cref="System.Numerics.BigInteger"/> type.
    /// Hex string representation uses string.
    /// The byte array representation uses byte[].
    /// Hex type supports casting, implicit casting, equality, and comparison with types BigInteger, string, byte[], long, int, uint, short, and ushort.
    /// </summary>
    public struct Hex
    {
        private static readonly Regex HexRegex = new Regex("^(0x)?[0-9a-fA-F]+$");


        private BigInteger? _integer;

        public BigInteger Integer
        {
            get
            {
                if (_integer == null)
                {
                    if (_bytes != null)
                        _integer = HexConvert.BytesToInt(_bytes);
                    else if (_hexString != null)
                        _integer = HexConvert.StringToInt(_hexString);
                    else
                        _integer = 0;
                }

                return _integer.Value;
            }
            set
            {
                InitializeWithBigInteger(value);
                _hexString = null;
                _bytes = null;
            }
        }

        private string? _hexString;

        public string HexString
        {
            get
            {
                if (_hexString == null)
                {
                    if (_bytes != null)
                        _hexString = HexConvert.BytesToString(_bytes, true, true);
                    else if (_integer != null)
                        _hexString = HexConvert.IntToString(_integer.Value);
                    else
                        _hexString = "0x0";
                }

                return _hexString;
            }
            set
            {
                InitializeWithHexString(value);
                _integer = null;
                _bytes = null;
            }
        }

        private byte[]? _bytes;

        public byte[] Bytes
        {
            get
            {
                if (_bytes == null)
                {
                    if (_hexString != null)
                        _bytes = HexConvert.StringToBytes(_hexString);
                    else if (_integer != null)
                        _bytes = HexConvert.IntToBytes(_integer.Value);
                    else
                        _bytes = new byte[] {0};
                }

                return _bytes;
            }
            set
            {
                _bytes = value;
                _integer = null;
                _hexString = null;
            }
        }

        /// <summary>
        /// Creates a Hex from a BigInteger.
        /// </summary>
        /// <param name="bigInteger">Non-negative integer</param>
        /// <exception cref="ArgumentException">If the bigInteger is negative</exception>
        public Hex(BigInteger bigInteger)
        {
            _integer = null;
            _hexString = null;
            _bytes = null;
            InitializeWithBigInteger(bigInteger);
        }

        /// <summary>
        /// Creates a Hex from a hex string.
        /// </summary>
        /// <param name="hexString"> Big endian, unsigned, hex string representation without leading zeros. Does not need to have 0x at the start. Can have small or capital letters. </param>
        /// <exception cref="ArgumentException">If the hexString is invalid</exception>
        public Hex(string hexString)
        {
            _integer = null;
            _hexString = null;
            _bytes = null;
            InitializeWithHexString(hexString);
        }

        /// <summary>
        /// Creates a hex from a byte array.
        /// </summary>
        /// <param name="bytes">Big endian, unsigned hex representation</param>
        public Hex(byte[] bytes)
        {
            _bytes = bytes;
            _integer = null;
            _hexString = null;
        }

        private void InitializeWithBigInteger(BigInteger bigInteger)
        {
            if (bigInteger.Sign < 0)
                throw new ArgumentException("Hex does not support negative values.");

            _integer = bigInteger;
        }

        private void InitializeWithHexString(string hexString)
        {
            if (!HexRegex.IsMatch(hexString))
                throw new ArgumentException($"Hex string is not valid: {hexString}.");

            if (!hexString.StartsWith("0x"))
                hexString = "0x" + hexString.ToLower();

            _hexString = hexString;
        }

        public static implicit operator Hex(BigInteger bigInteger) => new Hex(bigInteger);
        public static implicit operator Hex(string hexString) => new Hex(hexString);
        public static implicit operator Hex(byte[] bytes) => new Hex(bytes);
        public static implicit operator Hex(long value) => new Hex(value);
        public static implicit operator Hex(ulong value) => new Hex(value);
        public static implicit operator Hex(int value) => new Hex(value);
        public static implicit operator Hex(uint value) => new Hex(value);
        public static implicit operator Hex(short value) => new Hex(value);
        public static implicit operator Hex(ushort value) => new Hex(value);
        public static implicit operator BigInteger(Hex hex) => hex.Integer;
        public static implicit operator string(Hex hex) => hex.HexString;
        public static implicit operator byte[](Hex hex) => hex.Bytes;

        public static implicit operator long(Hex hex)
        {
            if (hex.Integer > long.MaxValue)
                throw new ImplicitOperatorException("Hex value cannot be implicitly converted to long because it overflows");
            return (long) hex.Integer;
        }

        public static implicit operator ulong(Hex hex)
        {
            if (hex.Integer > ulong.MaxValue)
                throw new ImplicitOperatorException("Hex value cannot be implicitly converted to ulong because it overflows");
            return (ulong) hex.Integer;
        }

        public static implicit operator int(Hex hex)
        {
            if (hex.Integer > int.MaxValue)
                throw new ImplicitOperatorException("Hex value cannot be implicitly converted to int because it overflows");
            return (int) hex.Integer;
        }

        public static implicit operator uint(Hex hex)
        {
            if (hex.Integer > uint.MaxValue)
                throw new ImplicitOperatorException("Hex value cannot be implicitly converted to uint because it overflows");
            return (uint) hex.Integer;
        }

        public static implicit operator short(Hex hex)
        {
            if (hex.Integer > short.MaxValue)
                throw new ImplicitOperatorException("Hex value cannot be implicitly converted to short because it overflows");
            return (short) hex.Integer;
        }

        public static implicit operator ushort(Hex hex)
        {
            if (hex.Integer > ushort.MaxValue)
                throw new ImplicitOperatorException("Hex value cannot be implicitly converted to ushort because it overflows");
            return (ushort) hex.Integer;
        }

        public bool Equals(Hex other) => Integer.Equals(other.Integer);

        public override bool Equals(object? obj)
        {
            return obj switch
            {
                Hex other => Equals(other),
                BigInteger bigInteger => Integer.Equals(bigInteger),
                string hexString => HexString.Equals(hexString),
                byte[] bytes => Integer.Equals(new BigInteger(bytes, true, true)),
                long l => Integer.Equals(l),
                ulong ul => Integer.Equals(ul),
                int i => Integer.Equals(i),
                uint ui => Integer.Equals(ui),
                short s => Integer.Equals(s),
                ushort us => Integer.Equals(us),
                _ => false,
            };
        }

        public override int GetHashCode() => Integer.GetHashCode();

        public static Hex operator +(Hex a, Hex b) => a.Integer + b.Integer;
        public static Hex operator -(Hex a, Hex b) => a.Integer - b.Integer;
        public static Hex operator *(Hex a, Hex b) => a.Integer * b.Integer;
        public static Hex operator /(Hex a, Hex b) => a.Integer / b.Integer;
        public static Hex operator %(Hex a, Hex b) => a.Integer % b.Integer;
        public static Hex operator ++(Hex a) => a.Integer + 1;
        public static Hex operator --(Hex a) => a.Integer - 1;
        public static bool operator ==(Hex left, Hex right) => left.Integer.Equals(right.Integer);
        public static bool operator !=(Hex left, Hex right) => !left.Integer.Equals(right.Integer);
        public static bool operator <(Hex left, Hex right) => left.Integer.CompareTo(right.Integer) < 0;
        public static bool operator >(Hex left, Hex right) => left.Integer.CompareTo(right.Integer) > 0;
        public static bool operator <=(Hex left, Hex right) => left.Integer.CompareTo(right.Integer) <= 0;
        public static bool operator >=(Hex left, Hex right) => left.Integer.CompareTo(right.Integer) >= 0;

        public override string ToString() => HexString;
        public string ToString(string? format) => Integer.ToString(format);
        public string ToString(IFormatProvider? formatProvider) => Integer.ToString(formatProvider);
        public string ToString(string? format, IFormatProvider? formatProvider) => Integer.ToString(format, formatProvider);

        public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
        {
            var result = Integer.TryFormat(destination, out int innerCharsWritten, format, provider);
            charsWritten = innerCharsWritten;
            return result;
        }
    }

    public class ImplicitOperatorException : Exception
    {
        public ImplicitOperatorException(string message) : base(message)
        {
        }
    }

    /// <summary>
    /// Hex conversion helper methods.
    /// Numbers are unsigned and big endian.
    /// Hex strings are prefixed by default and leading zeros not discarded by default.
    /// </summary>
    public static class HexConvert
    {
        public static byte[] IntToBytes(BigInteger n) => n.ToByteArray(true, true);
        public static BigInteger BytesToInt(byte[] bytes) => new BigInteger(bytes, true, true);

        public static byte[] StringToBytes(string str, bool trimZeros = false)
        {
            // Find the start offset
            var startOffset = str.StartsWith("0x") ? 2 : 0;
            if (trimZeros)
                for (; startOffset < str.Length - 1; startOffset++)
                    if (str[startOffset] != '0')
                        break;
            
            // Construct bytes array
            var bytes = new byte[(str.Length - startOffset + 1) / 2];
            
            for (var i = str.Length - startOffset - 1; i > 0; i -= 2)
                bytes[i / 2] = Convert.ToByte(str.Substring(i - 1 + startOffset, 2), 16);

            // If there are odd number of characters, add the first character to the first byte
            if ((str.Length - startOffset) % 2 == 1)
                bytes[0] = Convert.ToByte(str[startOffset].ToString(), 16);

            return bytes;
        }

        public static string BytesToString(byte[] bytes, bool prefix = true, bool trimZero = false)
        {
            var sb = new StringBuilder();

            foreach (var b in bytes)
                sb.Append(b.ToString("x2"));

            if (trimZero)
            {
                var start = 0;
                for (; start < sb.Length - 1; start++)
                    if (sb[start] != '0')
                        break;
                
                sb.Remove(0, start);
            }
            
            if (prefix)
                sb.Insert(0, "0x");
            
            return sb.ToString();
        }

        public static BigInteger StringToInt(string str) => BytesToInt(StringToBytes(str));

        public static string IntToString(BigInteger n, bool prefix = true) => BytesToString(IntToBytes(n), prefix, true);

        public static Hex ToHex(this BigInteger n) => new Hex(n);
        public static Hex ToHex(this string hex) => new Hex(hex);
        public static Hex ToHex(this byte[] b) => new Hex(b);
        public static Hex ToHex(this long n) => new Hex(n);
        public static Hex ToHex(this ulong n) => new Hex(n);
        public static Hex ToHex(this int n) => new Hex(n);
        public static Hex ToHex(this uint n) => new Hex(n);
        public static Hex ToHex(this short n) => new Hex(n);
        public static Hex ToHex(this ushort n) => new Hex(n);

        public static Hex Sha3(this Hex hex) => new Hex(Sha3Keccack.Current.CalculateHash(hex.Bytes));
    }

    public static class HexUtils
    {
        public static string RemoveLeadingZeros(string hex)
        {
            var start = 0;
            if (hex.StartsWith("0x"))
                start = 2;

            if (hex.Length - start <= 1 || hex[start] != '0')
                return hex;

            var sb = new StringBuilder();
            sb.Append(hex[..start]);

            for (var i = start; i < hex.Length - 1; i++)
            {
                if (hex[i] != '0')
                    break;
                start++;
            }

            sb.Append(hex[start..]);
            return sb.ToString();
        }

        public static string AppendLeadingZeros(string hex, int length)
        {
            var start = 0;
            if (hex.StartsWith("0x"))
                start = 2;

            if (hex.Length - start >= length)
                return hex;

            var sb = new StringBuilder();
            sb.Append(hex[..start]);

            var appendLength = length - hex.Length + start;
            for (var i = 0; i < appendLength; i++)
                sb.Append('0');

            sb.Append(hex[start..]);
            return sb.ToString();
        }

        public static string SetLength(string hex, int length)
        {
            var start = 0;
            if (hex.StartsWith("0x"))
                start = 2;

            if (hex.Length - start == length)
                return hex;

            var sb = new StringBuilder();
            sb.Append(hex[..start]);

            if (hex.Length - start > length)
                sb.Append(hex[^length..]);
            else
            {
                var appendLength = length - hex.Length + start;
                for (var i = 0; i < appendLength; i++)
                    sb.Append('0');

                sb.Append(hex[start..]);
            }

            return sb.ToString();
        }
    }

    public class HexJsonConverter : JsonConverter<Hex>
    {
        public override Hex Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return reader.GetString()!;
        }

        public override void Write(Utf8JsonWriter writer, Hex value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}