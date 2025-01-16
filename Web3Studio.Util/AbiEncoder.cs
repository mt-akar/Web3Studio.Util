using System;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;

namespace Web3Studio.Util
{
    public static class AbiEncoder
    {
        #region Encode

        public static string Encode<TTuple>(TTuple tuple)
            where TTuple : ITuple
        {
            return EncodeWithPrefix("0x", tuple);
        }

        public static string Encode<TTuple>(string methodHash, TTuple tuple)
            where TTuple : ITuple
        {
            return EncodeWithPrefix($"0x{methodHash}", tuple);
        }

        private static string EncodeWithPrefix<TTuple>(string prefix, TTuple tuple)
            where TTuple : ITuple
        {
            var sb = new StringBuilder();
            sb.Append(prefix);

            AppendEncodedTuple(sb, tuple);
            return sb.ToString();
        }

        public static string EncodeObjects(object[] objects)
        {
            return EncodeObjectsWithPrefix("0x", objects);
        }

        public static string EncodeObjects(string methodHash, object[] objects)
        {
            return EncodeObjectsWithPrefix($"0x{methodHash}", objects);
        }

        private static string EncodeObjectsWithPrefix(string prefix, object[] objects)
        {
            var sb = new StringBuilder();
            sb.Append(prefix);
            var sbDynamic = new StringBuilder();
            foreach (var value in objects)
                AppendEncodedTupleItem(sb, sbDynamic, value, objects.Length);

            sb.Append(sbDynamic);
            return sb.ToString();
        }

        private static void AppendEncodedTupleItem(
            StringBuilder sbValue,
            StringBuilder sbDynamic,
            object? value,
            int length)
        {
            void AppendPosition() => AppendEncodedValue(sbValue, length * 32 + sbDynamic.Length / 2);

            switch (value)
            {
                case null:
                    AppendEncodedValue(sbValue, 0);
                    break;
                case string stringValue:
                    AppendPosition();
                    var bytes = Encoding.ASCII.GetBytes(stringValue);
                    AppendEncodedByteArray(sbDynamic, bytes);
                    break;
                case byte[] byteArrayValue:
                    AppendPosition();
                    AppendEncodedByteArray(sbDynamic, byteArrayValue);
                    break;
                case ITuple tuple:
                    AppendPosition();
                    AppendEncodedTuple(sbDynamic, tuple);
                    break;
                case Array array:
                    AppendPosition();
                    AppendEncodedValue(sbDynamic, array.Length);
                    AppendEncodedArray(sbDynamic, array);
                    break;
                default:
                    AppendEncodedValue(sbValue, value);
                    break;
            }
        }

        private static void AppendEncodedByteArray(StringBuilder sb, byte[] bytes)
        {
            AppendEncodedInt(sb, bytes.Length);

            var hexString = HexConvert.BytesToString(bytes, false, false);
            sb.Append(hexString);
            sb.Append('0', 64 - hexString.Length % 64);
        }

        private static void AppendEncodedTuple(StringBuilder sb, ITuple tuple)
        {
            var sbDynamic = new StringBuilder();
            for (var i = 0; i < tuple.Length; i++)
                AppendEncodedTupleItem(sb, sbDynamic, tuple[i], tuple.Length);

            sb.Append(sbDynamic);
        }

        private static void AppendEncodedArray(StringBuilder sb, Array array)
        {
            var sbDynamic = new StringBuilder();
            foreach (var value in array)
                AppendEncodedTupleItem(sb, sbDynamic, value, array.Length);

            sb.Append(sbDynamic);
        }

        private static void AppendEncodedValue(StringBuilder sb, object value)
        {
            if (value is Hex hexValue) AppendEncodedInt(sb, hexValue.Integer);
            else if (value is bool boolValue) AppendEncodedInt(sb, boolValue ? 1 : 0);
            else if (value is BigInteger bigIntegerValue) AppendEncodedInt(sb, bigIntegerValue);
            else if (value is int intValue) AppendEncodedInt(sb, intValue);
            else if (value is long longValue) AppendEncodedInt(sb, longValue);
            else if (value is uint uintValue) AppendEncodedInt(sb, uintValue);
            else if (value is ulong ulongValue) AppendEncodedInt(sb, ulongValue);
            else if (value is byte byteValue) AppendEncodedInt(sb, byteValue);
            else if (value is sbyte sbyteValue) AppendEncodedInt(sb, sbyteValue);
            else throw new NotSupportedException($"Type {value.GetType()} is not supported");
        }

        private static void AppendEncodedInt(StringBuilder sb, BigInteger value)
        {
            var hex = HexConvert.IntToString(value, false);
            if (hex.Length > 64)
                throw new Exception("ABI encoding numbers larger than 32 bytes are not supported");
            sb.Append('0', 64 - hex.Length);
            sb.Append(hex);
        }

        #endregion

        #region Decode

        public static TTuple DecodeToValueTuple<TTuple>(string data)
            where TTuple : ITuple
        {
            var prefixLength = data.StartsWith("0x") ? 2 : 0;
            var decodedData = typeof(TTuple).GetGenericArguments().Select<Type, object>((arg, i) =>
            {
                if (arg == typeof(Hex))
                    return DecodeToHex(data.AsSpan()[prefixLength..], i * 64);
                if (arg == typeof(bool))
                    return DecodeBool(data.AsSpan()[prefixLength..], i * 64);
                if (arg == typeof(string))
                    return DecodeString(data.AsSpan()[prefixLength..], i * 64);

                throw new ArgumentException("ValueTuple generic argument is not supported");
            }).ToArray();

            return (TTuple)Activator.CreateInstance(typeof(TTuple), decodedData)!;
        }

        public static Hex DecodeToHex(string hex)
        {
            var prefixLength = hex.StartsWith("0x") ? 2 : 0;
            return DecodeToHex(hex.AsSpan()[prefixLength..], 0);
        }

        public static string DecodeString(string data)
        {
            var prefixLength = data.StartsWith("0x") ? 2 : 0;
            return DecodeString(data.AsSpan()[prefixLength..], 0);
        }

        public static bool DecodeBool(string data)
        {
            var prefixLength = data.StartsWith("0x") ? 2 : 0;
            return DecodeBool(data.AsSpan()[prefixLength..], 0);
        }

        private static Hex DecodeToHex(ReadOnlySpan<char> hex, int offset)
        {
            if (offset + 64 > hex.Length)
                throw new FormatException("Parameter length can't be larger than hex string length.");

            var slice = hex[offset..(offset + 64)].TrimStart('0');
            if (slice.Length == 0)
                return 0;

            return slice.ToString();
        }

        private static bool DecodeBool(ReadOnlySpan<char> hex, int offset)
        {
            return hex[offset + 63] != '0';
        }

        private static string DecodeString(ReadOnlySpan<char> hex, int offset)
        {
            int lengthOffset = DecodeToHex(hex, offset);
            var stringStart = lengthOffset * 2 + 64;
            var stringEnd = stringStart + DecodeToHex(hex, lengthOffset * 2) * 2;

            var stringBuilder = new StringBuilder();
            for (var i = stringStart; i < stringEnd; i += 2)
                stringBuilder.Append((char)Convert.ToByte(hex[i..(i + 2)].ToString(), 16));

            return stringBuilder.ToString();
        }

        private static object DecodeArray(ReadOnlySpan<char> hex, int offset)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}