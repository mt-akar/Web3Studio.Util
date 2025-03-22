using System.Globalization;
using System.Linq;
using System.Numerics;

namespace Web3Studio.Util.Tests;

// Problem 1: Hexadecimal strings on Eth -> integer types on C#
// Problem 2: Big numbers
// Strength 1: Simple storing & converting
// Strength 2: Simple creation
// Strength 3: Cast & implicit cast
// Strength 4: Lazy
public sealed class HexTests
{
    #region Hex type introduction

    [Fact]
    public void Conversions()
    {
        BigInteger int285 = HexConvert.StringToInt("0x11d");
        string string285 = HexConvert.BytesToString(new byte[] {0x11, 0xd});

        Hex hex285 = new Hex(285);
        Hex hex285FromHex = new Hex("0x11d");
        Hex hex285FromBytes = new Hex(new byte[] {0x11, 0xd});
        BigInteger hex285Int = hex285.Integer;
        string hex10String = hex285.HexString;
        byte[] hex10Bytes = hex285.Bytes;

        int hex285IntCast = (int) hex285;
        string hex285StringCast = (string) hex285;
        byte[] hex285BytesCast = (byte[]) hex285;
    }

    public long DoubleInt(int x)
    {
        return (long) x * 2;
    }

    public Hex DoubleHex(Hex x)
    {
        return x * 2;
    }

    [Fact]
    public void ImplicitCasts()
    {
        Hex hex10 = 10;
        Hex hex10FromString = "0xa";
        Hex hex10FromBytes = new byte[] {0xa};

        int int10 = hex10FromBytes;
        ulong ulong10 = hex10;
        string string10 = hex10;
        byte[] bytes10 = hex10;

        Hex hex20 = DoubleInt(hex10FromString);
        short sbyte20 = DoubleHex(ulong10);
    }

    [Fact]
    public void Arithmetic()
    {
        Hex hex10 = 10;
        Hex hex20 = hex10 + hex10;
        Hex hex400 = hex20 * hex20;
        Hex hex100 = hex400 - 300;
        Hex hex14 = hex100 / 7;
        Hex hex2 = hex14 % 3;
        bool hex2Is = hex2 <= 2 && hex2 >= 2 && hex2 > 1 && hex2 < 3;
        bool hex20Is = hex20 < hex100 && hex20 > hex14;
    }

    #endregion

    [Fact]
    public void Casting_ImplicitCasting_CorrectlyCasts()
    {
        BigInteger ReturnBigInteger(BigInteger n) => n;
        Hex bigIntegerHex = BigInteger.Parse("12");
        BigInteger bigInteger = ReturnBigInteger(bigIntegerHex);
        bigInteger.Should().BeOfType(typeof(BigInteger));
        bigInteger.Should().Be(BigInteger.Parse("12"));
        ((BigInteger) bigIntegerHex).Should().Be(BigInteger.Parse("12"));

        string ReturnString(string s) => s;
        Hex stringHex = "0x12";
        string stringHexString = ReturnString(stringHex);
        stringHexString.Should().BeOfType(typeof(string));
        stringHexString.Should().Be("0x12");
        ((string) stringHex).Should().Be("0x12");

        byte[] ReturnByteArray(byte[] b) => b;
        Hex byteArrayHex = new byte[] {0x12};
        byte[] byteArray = ReturnByteArray(byteArrayHex);
        byteArray.Should().BeOfType(typeof(byte[]));
        byteArray.Should().BeEquivalentTo(new byte[] {0x12});
        ((byte[]) byteArrayHex).Should().BeEquivalentTo(new byte[] {0x12});

        ulong ReturnUlong(ulong n) => n;
        Hex ulongHex = 12UL;
        ulong @ulong = ReturnUlong(ulongHex);
        @ulong.Should().BeOfType(typeof(ulong));
        @ulong.Should().Be(12UL);
        ((ulong) ulongHex).Should().Be(12UL);

        long ReturnLong(long n) => n;
        Hex longHex = 12L;
        long @long = ReturnLong(longHex);
        @long.Should().BeOfType(typeof(long));
        @long.Should().Be(12L);
        ((long) longHex).Should().Be(12L);

        int ReturnInt(int n) => n;
        Hex intHex = 12;
        int @int = ReturnInt(intHex);
        @int.Should().BeOfType(typeof(int));
        @int.Should().Be(12);
        ((int) intHex).Should().Be(12);

        uint ReturnUint(uint n) => n;
        Hex uintHex = 12U;
        uint @uint = ReturnUint(uintHex);
        @uint.Should().BeOfType(typeof(uint));
        @uint.Should().Be(12U);
        ((uint) uintHex).Should().Be(12U);

        short ReturnShort(short n) => n;
        Hex shortHex = (short) 12;
        short @short = ReturnShort(shortHex);
        @short.Should().BeOfType(typeof(short));
        @short.Should().Be((short) 12);
        ((short) shortHex).Should().Be((short) 12);

        ushort ReturnUshort(ushort n) => n;
        Hex ushortHex = (ushort) 12;
        ushort @ushort = ReturnUshort(ushortHex);
        @ushort.Should().BeOfType(typeof(ushort));
        @ushort.Should().Be((ushort) 12);
        ((ushort) ushortHex).Should().Be((ushort) 12);
    }

    [Fact]
    public void EqualityChecks_Work()
    {
        Hex hex834Instance0 = 834;
        hex834Instance0.Integer.Should().Be(834);
        hex834Instance0.Integer.Should().Be(834L);
        hex834Instance0.Integer.Should().Be(834U);
        hex834Instance0.HexString.Should().Be("0x342");
        hex834Instance0.Should().Be(834);
        hex834Instance0.Should().Be(834L);
        hex834Instance0.Should().Be(834U);
        hex834Instance0.Should().Be("0x342");

        var hex834Instance1 = new Hex(834);
        hex834Instance1.Integer.Should().Be(834);
        hex834Instance1.Integer.Should().Be(834L);
        hex834Instance1.Integer.Should().Be(834U);
        hex834Instance1.HexString.Should().Be("0x342");
        hex834Instance1.Should().Be(834);
        hex834Instance1.Should().Be(834L);
        hex834Instance1.Should().Be(834U);
        hex834Instance1.Should().Be("0x342");

        Hex hex834Instance2 = 834;
        hex834Instance2.Integer.Should().Be(834);
        hex834Instance2.Integer.Should().Be(834L);
        hex834Instance2.Integer.Should().Be(834U);
        hex834Instance2.HexString.Should().Be("0x342");
        hex834Instance2.Should().Be(834);
        hex834Instance2.Should().Be(834L);
        hex834Instance2.Should().Be(834U);
        hex834Instance2.Should().Be("0x342");

        var hex834Instance3 = new Hex(834);
        hex834Instance3.Integer.Should().Be(834);
        hex834Instance3.Integer.Should().Be(834L);
        hex834Instance3.Integer.Should().Be(834U);
        hex834Instance3.HexString.Should().Be("0x342");
        hex834Instance3.Should().Be(834);
        hex834Instance3.Should().Be(834L);
        hex834Instance3.Should().Be(834U);
        hex834Instance3.Should().Be("0x342");

        Hex hex834Instance4 = 834;
        hex834Instance4.Integer.Should().Be(834);
        hex834Instance4.Integer.Should().Be(834L);
        hex834Instance4.Integer.Should().Be(834U);
        hex834Instance4.HexString.Should().Be("0x342");
        hex834Instance4.Should().Be(834);
        hex834Instance4.Should().Be(834L);
        hex834Instance4.Should().Be(834U);
        hex834Instance4.Should().Be("0x342");

        var hex834Instance5 = new Hex(834);
        hex834Instance5.Integer.Should().Be(834);
        hex834Instance5.Integer.Should().Be(834L);
        hex834Instance5.Integer.Should().Be(834U);
        hex834Instance5.HexString.Should().Be("0x342");
        hex834Instance5.Should().Be(834);
        hex834Instance5.Should().Be(834L);
        hex834Instance5.Should().Be(834U);
        hex834Instance5.Should().Be("0x342");

        Hex hex834Instance6 = "0x342";
        hex834Instance6.Integer.Should().Be(834);
        hex834Instance6.Integer.Should().Be(834L);
        hex834Instance6.Integer.Should().Be(834U);
        hex834Instance6.HexString.Should().Be("0x342");
        hex834Instance6.Should().Be(834);
        hex834Instance6.Should().Be(834L);
        hex834Instance6.Should().Be(834U);
        hex834Instance6.Should().Be("0x342");

        var hex834Instance7 = new Hex("0x342");
        hex834Instance7.Integer.Should().Be(834);
        hex834Instance7.Integer.Should().Be(834L);
        hex834Instance7.Integer.Should().Be(834U);
        hex834Instance7.HexString.Should().Be("0x342");
        hex834Instance7.Should().Be(834);
        hex834Instance7.Should().Be(834L);
        hex834Instance7.Should().Be(834U);
        hex834Instance7.Should().Be("0x342");

        Hex hex834Instance8 = "342";
        hex834Instance8.Integer.Should().Be(834);
        hex834Instance8.Integer.Should().Be(834L);
        hex834Instance8.Integer.Should().Be(834U);
        hex834Instance8.HexString.Should().Be("0x342");
        hex834Instance8.Should().Be(834);
        hex834Instance8.Should().Be(834L);
        hex834Instance8.Should().Be(834U);
        hex834Instance8.Should().Be("0x342");

        var hex834Instance9 = new Hex("342");
        hex834Instance9.Integer.Should().Be(834);
        hex834Instance9.Integer.Should().Be(834L);
        hex834Instance9.Integer.Should().Be(834U);
        hex834Instance9.HexString.Should().Be("0x342");
        hex834Instance9.Should().Be(834);
        hex834Instance9.Should().Be(834L);
        hex834Instance9.Should().Be(834U);
        hex834Instance9.Should().Be("0x342");

        Hex hex834InstanceA = BigInteger.Parse("834");
        hex834InstanceA.Integer.Should().Be(834);
        hex834InstanceA.Integer.Should().Be(834L);
        hex834InstanceA.Integer.Should().Be(834U);
        hex834InstanceA.HexString.Should().Be("0x342");
        hex834InstanceA.Should().Be(834);
        hex834InstanceA.Should().Be(834L);
        hex834InstanceA.Should().Be(834U);
        hex834InstanceA.Should().Be("0x342");

        var hex834InstanceB = new Hex(BigInteger.Parse("834"));
        hex834InstanceB.Integer.Should().Be(834);
        hex834InstanceB.Integer.Should().Be(834L);
        hex834InstanceB.Integer.Should().Be(834U);
        hex834InstanceB.HexString.Should().Be("0x342");
        hex834InstanceB.Should().Be(834);
        hex834InstanceB.Should().Be(834L);
        hex834InstanceB.Should().Be(834U);
        hex834InstanceB.Should().Be("0x342");

        Hex hex834InstanceC = ((short) 834);
        hex834InstanceC.Integer.Should().Be(834);
        hex834InstanceC.Integer.Should().Be(834L);
        hex834InstanceC.Integer.Should().Be(834U);
        hex834InstanceC.HexString.Should().Be("0x342");
        hex834InstanceC.Should().Be(834);
        hex834InstanceC.Should().Be(834L);
        hex834InstanceC.Should().Be(834U);
        hex834InstanceC.Should().Be("0x342");

        var hex834InstanceD = new Hex((short) 834);
        hex834InstanceD.Integer.Should().Be(834);
        hex834InstanceD.Integer.Should().Be(834L);
        hex834InstanceD.Integer.Should().Be(834U);
        hex834InstanceD.HexString.Should().Be("0x342");
        hex834InstanceD.Should().Be(834);
        hex834InstanceD.Should().Be(834L);
        hex834InstanceD.Should().Be(834U);
        hex834InstanceD.Should().Be("0x342");

        Hex hex834InstanceE = new byte[] {3, 66};
        hex834InstanceE.Integer.Should().Be(834);
        hex834InstanceE.Integer.Should().Be(834L);
        hex834InstanceE.Integer.Should().Be(834U);
        hex834InstanceE.HexString.Should().Be("0x342");
        hex834InstanceE.Should().Be(834);
        hex834InstanceE.Should().Be(834L);
        hex834InstanceE.Should().Be(834U);
        hex834InstanceE.Should().Be("0x342");

        var hex834InstanceF = new Hex([3, 66]);
        hex834InstanceF.Integer.Should().Be(834);
        hex834InstanceF.Integer.Should().Be(834L);
        hex834InstanceF.Integer.Should().Be(834U);
        hex834InstanceF.HexString.Should().Be("0x342");
        hex834InstanceF.Should().Be(834);
        hex834InstanceF.Should().Be(834L);
        hex834InstanceF.Should().Be(834U);
        hex834InstanceF.Should().Be("0x342");

        (hex834Instance0 == hex834Instance1).Should().BeTrue();
        (hex834Instance0 == hex834Instance2).Should().BeTrue();
        (hex834Instance0 == hex834Instance3).Should().BeTrue();
        (hex834Instance0 == hex834Instance4).Should().BeTrue();
        (hex834Instance0 == hex834Instance5).Should().BeTrue();
        (hex834Instance0 == hex834Instance6).Should().BeTrue();
        (hex834Instance0 == hex834Instance7).Should().BeTrue();
        (hex834Instance0 == hex834Instance8).Should().BeTrue();
        (hex834Instance0 == hex834Instance9).Should().BeTrue();
        (hex834Instance0 == hex834InstanceA).Should().BeTrue();
        (hex834Instance0 == hex834InstanceB).Should().BeTrue();
        (hex834Instance0 == hex834InstanceC).Should().BeTrue();
        (hex834Instance0 == hex834InstanceD).Should().BeTrue();
        (hex834Instance0 == hex834InstanceE).Should().BeTrue();
        (hex834Instance0 == hex834InstanceF).Should().BeTrue();
        (hex834Instance0 != hex834Instance1).Should().BeFalse();
        (hex834Instance0 != hex834Instance2).Should().BeFalse();
        (hex834Instance0 != hex834Instance3).Should().BeFalse();
        (hex834Instance0 != hex834Instance4).Should().BeFalse();
        (hex834Instance0 != hex834Instance5).Should().BeFalse();
        (hex834Instance0 != hex834Instance6).Should().BeFalse();
        (hex834Instance0 != hex834Instance7).Should().BeFalse();
        (hex834Instance0 != hex834Instance8).Should().BeFalse();
        (hex834Instance0 != hex834Instance9).Should().BeFalse();
        (hex834Instance0 != hex834InstanceA).Should().BeFalse();
        (hex834Instance0 != hex834InstanceB).Should().BeFalse();
        (hex834Instance0 != hex834InstanceC).Should().BeFalse();
        (hex834Instance0 != hex834InstanceD).Should().BeFalse();
        (hex834Instance0 != hex834InstanceE).Should().BeFalse();
        (hex834Instance0 != hex834InstanceF).Should().BeFalse();
        hex834Instance0.Equals(hex834Instance1).Should().BeTrue();
        hex834Instance0.Equals(hex834Instance2).Should().BeTrue();
        hex834Instance0.Equals(hex834Instance3).Should().BeTrue();
        hex834Instance0.Equals(hex834Instance4).Should().BeTrue();
        hex834Instance0.Equals(hex834Instance5).Should().BeTrue();
        hex834Instance0.Equals(hex834Instance6).Should().BeTrue();
        hex834Instance0.Equals(hex834Instance7).Should().BeTrue();
        hex834Instance0.Equals(hex834Instance8).Should().BeTrue();
        hex834Instance0.Equals(hex834Instance9).Should().BeTrue();
        hex834Instance0.Equals(hex834InstanceA).Should().BeTrue();
        hex834Instance0.Equals(hex834InstanceB).Should().BeTrue();
        hex834Instance0.Equals(hex834InstanceC).Should().BeTrue();
        hex834Instance0.Equals(hex834InstanceD).Should().BeTrue();
        hex834Instance0.Equals(hex834InstanceE).Should().BeTrue();
        hex834Instance0.Equals(hex834InstanceF).Should().BeTrue();

        (hex834Instance0.Integer == hex834Instance1.Integer).Should().BeTrue();
        (hex834Instance0.Integer == hex834Instance2.Integer).Should().BeTrue();
        (hex834Instance0.Integer == hex834Instance3.Integer).Should().BeTrue();
        (hex834Instance0.Integer == hex834Instance4.Integer).Should().BeTrue();
        (hex834Instance0.Integer == hex834Instance5.Integer).Should().BeTrue();
        (hex834Instance0.Integer == hex834Instance6.Integer).Should().BeTrue();
        (hex834Instance0.Integer == hex834Instance7.Integer).Should().BeTrue();
        (hex834Instance0.Integer == hex834Instance8.Integer).Should().BeTrue();
        (hex834Instance0.Integer == hex834Instance9.Integer).Should().BeTrue();
        (hex834Instance0.Integer == hex834InstanceA.Integer).Should().BeTrue();
        (hex834Instance0.Integer == hex834InstanceB.Integer).Should().BeTrue();
        (hex834Instance0.Integer == hex834InstanceC.Integer).Should().BeTrue();
        (hex834Instance0.Integer == hex834InstanceD.Integer).Should().BeTrue();
        (hex834Instance0.Integer == hex834InstanceE.Integer).Should().BeTrue();
        (hex834Instance0.Integer == hex834InstanceF.Integer).Should().BeTrue();
        (hex834Instance0.Integer != hex834Instance1.Integer).Should().BeFalse();
        (hex834Instance0.Integer != hex834Instance2.Integer).Should().BeFalse();
        (hex834Instance0.Integer != hex834Instance3.Integer).Should().BeFalse();
        (hex834Instance0.Integer != hex834Instance4.Integer).Should().BeFalse();
        (hex834Instance0.Integer != hex834Instance5.Integer).Should().BeFalse();
        (hex834Instance0.Integer != hex834Instance6.Integer).Should().BeFalse();
        (hex834Instance0.Integer != hex834Instance7.Integer).Should().BeFalse();
        (hex834Instance0.Integer != hex834Instance8.Integer).Should().BeFalse();
        (hex834Instance0.Integer != hex834Instance9.Integer).Should().BeFalse();
        (hex834Instance0.Integer != hex834InstanceA.Integer).Should().BeFalse();
        (hex834Instance0.Integer != hex834InstanceB.Integer).Should().BeFalse();
        (hex834Instance0.Integer != hex834InstanceC.Integer).Should().BeFalse();
        (hex834Instance0.Integer != hex834InstanceD.Integer).Should().BeFalse();
        (hex834Instance0.Integer != hex834InstanceE.Integer).Should().BeFalse();
        (hex834Instance0.Integer != hex834InstanceF.Integer).Should().BeFalse();
        hex834Instance0.Integer.Equals(hex834Instance1.Integer).Should().BeTrue();
        hex834Instance0.Integer.Equals(hex834Instance2.Integer).Should().BeTrue();
        hex834Instance0.Integer.Equals(hex834Instance3.Integer).Should().BeTrue();
        hex834Instance0.Integer.Equals(hex834Instance4.Integer).Should().BeTrue();
        hex834Instance0.Integer.Equals(hex834Instance5.Integer).Should().BeTrue();
        hex834Instance0.Integer.Equals(hex834Instance6.Integer).Should().BeTrue();
        hex834Instance0.Integer.Equals(hex834Instance7.Integer).Should().BeTrue();
        hex834Instance0.Integer.Equals(hex834Instance8.Integer).Should().BeTrue();
        hex834Instance0.Integer.Equals(hex834Instance9.Integer).Should().BeTrue();
        hex834Instance0.Integer.Equals(hex834InstanceA.Integer).Should().BeTrue();
        hex834Instance0.Integer.Equals(hex834InstanceB.Integer).Should().BeTrue();
        hex834Instance0.Integer.Equals(hex834InstanceC.Integer).Should().BeTrue();
        hex834Instance0.Integer.Equals(hex834InstanceD.Integer).Should().BeTrue();
        hex834Instance0.Integer.Equals(hex834InstanceE.Integer).Should().BeTrue();
        hex834Instance0.Integer.Equals(hex834InstanceF.Integer).Should().BeTrue();

        hex834Instance0.Integer.Equals(hex834Instance1).Should().BeTrue();
        hex834Instance0.Integer.Equals(hex834Instance2).Should().BeTrue();
        hex834Instance0.Integer.Equals(hex834Instance3).Should().BeTrue();
        hex834Instance0.Integer.Equals(hex834Instance4).Should().BeTrue();
        hex834Instance0.Integer.Equals(hex834Instance5).Should().BeTrue();
        hex834Instance0.Integer.Equals(hex834Instance6).Should().BeTrue();
        hex834Instance0.Integer.Equals(hex834Instance7).Should().BeTrue();
        hex834Instance0.Integer.Equals(hex834Instance8).Should().BeTrue();
        hex834Instance0.Integer.Equals(hex834Instance9).Should().BeTrue();
        hex834Instance0.Integer.Equals(hex834InstanceA).Should().BeTrue();
        hex834Instance0.Integer.Equals(hex834InstanceB).Should().BeTrue();
        hex834Instance0.Integer.Equals(hex834InstanceC).Should().BeTrue();
        hex834Instance0.Integer.Equals(hex834InstanceD).Should().BeTrue();
        hex834Instance0.Integer.Equals(hex834InstanceE).Should().BeTrue();
        hex834Instance0.Integer.Equals(hex834InstanceF).Should().BeTrue();

        hex834Instance0.Equals(hex834Instance1.Integer).Should().BeTrue();
        hex834Instance0.Equals(hex834Instance2.Integer).Should().BeTrue();
        hex834Instance0.Equals(hex834Instance3.Integer).Should().BeTrue();
        hex834Instance0.Equals(hex834Instance4.Integer).Should().BeTrue();
        hex834Instance0.Equals(hex834Instance5.Integer).Should().BeTrue();
        hex834Instance0.Equals(hex834Instance6.Integer).Should().BeTrue();
        hex834Instance0.Equals(hex834Instance7.Integer).Should().BeTrue();
        hex834Instance0.Equals(hex834Instance8.Integer).Should().BeTrue();
        hex834Instance0.Equals(hex834Instance9.Integer).Should().BeTrue();
        hex834Instance0.Equals(hex834InstanceA.Integer).Should().BeTrue();
        hex834Instance0.Equals(hex834InstanceB.Integer).Should().BeTrue();
        hex834Instance0.Equals(hex834InstanceC.Integer).Should().BeTrue();
        hex834Instance0.Equals(hex834InstanceD.Integer).Should().BeTrue();
        hex834Instance0.Equals(hex834InstanceE.Integer).Should().BeTrue();
        hex834Instance0.Equals(hex834InstanceF.Integer).Should().BeTrue();

        (hex834Instance0.HexString == hex834Instance1.HexString).Should().BeTrue();
        (hex834Instance0.HexString == hex834Instance2.HexString).Should().BeTrue();
        (hex834Instance0.HexString == hex834Instance3.HexString).Should().BeTrue();
        (hex834Instance0.HexString == hex834Instance4.HexString).Should().BeTrue();
        (hex834Instance0.HexString == hex834Instance5.HexString).Should().BeTrue();
        (hex834Instance0.HexString == hex834Instance6.HexString).Should().BeTrue();
        (hex834Instance0.HexString == hex834Instance7.HexString).Should().BeTrue();
        (hex834Instance0.HexString == hex834Instance8.HexString).Should().BeTrue();
        (hex834Instance0.HexString == hex834Instance9.HexString).Should().BeTrue();
        (hex834Instance0.HexString == hex834InstanceA.HexString).Should().BeTrue();
        (hex834Instance0.HexString == hex834InstanceB.HexString).Should().BeTrue();
        (hex834Instance0.HexString == hex834InstanceC.HexString).Should().BeTrue();
        (hex834Instance0.HexString == hex834InstanceD.HexString).Should().BeTrue();
        (hex834Instance0.HexString == hex834InstanceE.HexString).Should().BeTrue();
        (hex834Instance0.HexString == hex834InstanceF.HexString).Should().BeTrue();
        (hex834Instance0.HexString != hex834Instance1.HexString).Should().BeFalse();
        (hex834Instance0.HexString != hex834Instance2.HexString).Should().BeFalse();
        (hex834Instance0.HexString != hex834Instance3.HexString).Should().BeFalse();
        (hex834Instance0.HexString != hex834Instance4.HexString).Should().BeFalse();
        (hex834Instance0.HexString != hex834Instance5.HexString).Should().BeFalse();
        (hex834Instance0.HexString != hex834Instance6.HexString).Should().BeFalse();
        (hex834Instance0.HexString != hex834Instance7.HexString).Should().BeFalse();
        (hex834Instance0.HexString != hex834Instance8.HexString).Should().BeFalse();
        (hex834Instance0.HexString != hex834Instance9.HexString).Should().BeFalse();
        (hex834Instance0.HexString != hex834InstanceA.HexString).Should().BeFalse();
        (hex834Instance0.HexString != hex834InstanceB.HexString).Should().BeFalse();
        (hex834Instance0.HexString != hex834InstanceC.HexString).Should().BeFalse();
        (hex834Instance0.HexString != hex834InstanceD.HexString).Should().BeFalse();
        (hex834Instance0.HexString != hex834InstanceE.HexString).Should().BeFalse();
        (hex834Instance0.HexString != hex834InstanceF.HexString).Should().BeFalse();
        hex834Instance0.HexString.Equals(hex834Instance1.HexString).Should().BeTrue();
        hex834Instance0.HexString.Equals(hex834Instance2.HexString).Should().BeTrue();
        hex834Instance0.HexString.Equals(hex834Instance3.HexString).Should().BeTrue();
        hex834Instance0.HexString.Equals(hex834Instance4.HexString).Should().BeTrue();
        hex834Instance0.HexString.Equals(hex834Instance5.HexString).Should().BeTrue();
        hex834Instance0.HexString.Equals(hex834Instance6.HexString).Should().BeTrue();
        hex834Instance0.HexString.Equals(hex834Instance7.HexString).Should().BeTrue();
        hex834Instance0.HexString.Equals(hex834Instance8.HexString).Should().BeTrue();
        hex834Instance0.HexString.Equals(hex834Instance9.HexString).Should().BeTrue();
        hex834Instance0.HexString.Equals(hex834InstanceA.HexString).Should().BeTrue();
        hex834Instance0.HexString.Equals(hex834InstanceB.HexString).Should().BeTrue();
        hex834Instance0.HexString.Equals(hex834InstanceC.HexString).Should().BeTrue();
        hex834Instance0.HexString.Equals(hex834InstanceD.HexString).Should().BeTrue();
        hex834Instance0.HexString.Equals(hex834InstanceE.HexString).Should().BeTrue();
        hex834Instance0.HexString.Equals(hex834InstanceF.HexString).Should().BeTrue();

        hex834Instance0.Bytes.SequenceEqual(hex834Instance1.Bytes).Should().BeTrue();
        hex834Instance0.Bytes.SequenceEqual(hex834Instance2.Bytes).Should().BeTrue();
        hex834Instance0.Bytes.SequenceEqual(hex834Instance3.Bytes).Should().BeTrue();
        hex834Instance0.Bytes.SequenceEqual(hex834Instance4.Bytes).Should().BeTrue();
        hex834Instance0.Bytes.SequenceEqual(hex834Instance5.Bytes).Should().BeTrue();
        hex834Instance0.Bytes.SequenceEqual(hex834Instance6.Bytes).Should().BeTrue();
        hex834Instance0.Bytes.SequenceEqual(hex834Instance7.Bytes).Should().BeTrue();
        hex834Instance0.Bytes.SequenceEqual(hex834Instance8.Bytes).Should().BeTrue();
        hex834Instance0.Bytes.SequenceEqual(hex834Instance9.Bytes).Should().BeTrue();
        hex834Instance0.Bytes.SequenceEqual(hex834InstanceA.Bytes).Should().BeTrue();
        hex834Instance0.Bytes.SequenceEqual(hex834InstanceB.Bytes).Should().BeTrue();
        hex834Instance0.Bytes.SequenceEqual(hex834InstanceC.Bytes).Should().BeTrue();
        hex834Instance0.Bytes.SequenceEqual(hex834InstanceD.Bytes).Should().BeTrue();
        hex834Instance0.Bytes.SequenceEqual(hex834InstanceE.Bytes).Should().BeTrue();
        hex834Instance0.Bytes.SequenceEqual(hex834InstanceF.Bytes).Should().BeTrue();

        ((byte[]) hex834Instance0).SequenceEqual(hex834Instance1.Bytes).Should().BeTrue();
        ((byte[]) hex834Instance0).SequenceEqual(hex834Instance2.Bytes).Should().BeTrue();
        ((byte[]) hex834Instance0).SequenceEqual(hex834Instance3.Bytes).Should().BeTrue();
        ((byte[]) hex834Instance0).SequenceEqual(hex834Instance4.Bytes).Should().BeTrue();
        ((byte[]) hex834Instance0).SequenceEqual(hex834Instance5.Bytes).Should().BeTrue();
        ((byte[]) hex834Instance0).SequenceEqual(hex834Instance6.Bytes).Should().BeTrue();
        ((byte[]) hex834Instance0).SequenceEqual(hex834Instance7.Bytes).Should().BeTrue();
        ((byte[]) hex834Instance0).SequenceEqual(hex834Instance8.Bytes).Should().BeTrue();
        ((byte[]) hex834Instance0).SequenceEqual(hex834Instance9.Bytes).Should().BeTrue();
        ((byte[]) hex834Instance0).SequenceEqual(hex834InstanceA.Bytes).Should().BeTrue();
        ((byte[]) hex834Instance0).SequenceEqual(hex834InstanceB.Bytes).Should().BeTrue();
        ((byte[]) hex834Instance0).SequenceEqual(hex834InstanceC.Bytes).Should().BeTrue();
        ((byte[]) hex834Instance0).SequenceEqual(hex834InstanceD.Bytes).Should().BeTrue();
        ((byte[]) hex834Instance0).SequenceEqual(hex834InstanceE.Bytes).Should().BeTrue();
        ((byte[]) hex834Instance0).SequenceEqual(hex834InstanceF.Bytes).Should().BeTrue();

        hex834Instance0.Bytes.SequenceEqual((byte[]) hex834Instance1).Should().BeTrue();
        hex834Instance0.Bytes.SequenceEqual((byte[]) hex834Instance2).Should().BeTrue();
        hex834Instance0.Bytes.SequenceEqual((byte[]) hex834Instance3).Should().BeTrue();
        hex834Instance0.Bytes.SequenceEqual((byte[]) hex834Instance4).Should().BeTrue();
        hex834Instance0.Bytes.SequenceEqual((byte[]) hex834Instance5).Should().BeTrue();
        hex834Instance0.Bytes.SequenceEqual((byte[]) hex834Instance6).Should().BeTrue();
        hex834Instance0.Bytes.SequenceEqual((byte[]) hex834Instance7).Should().BeTrue();
        hex834Instance0.Bytes.SequenceEqual((byte[]) hex834Instance8).Should().BeTrue();
        hex834Instance0.Bytes.SequenceEqual((byte[]) hex834Instance9).Should().BeTrue();
        hex834Instance0.Bytes.SequenceEqual((byte[]) hex834InstanceA).Should().BeTrue();
        hex834Instance0.Bytes.SequenceEqual((byte[]) hex834InstanceB).Should().BeTrue();
        hex834Instance0.Bytes.SequenceEqual((byte[]) hex834InstanceC).Should().BeTrue();
        hex834Instance0.Bytes.SequenceEqual((byte[]) hex834InstanceD).Should().BeTrue();
        hex834Instance0.Bytes.SequenceEqual((byte[]) hex834InstanceE).Should().BeTrue();
        hex834Instance0.Bytes.SequenceEqual((byte[]) hex834InstanceF).Should().BeTrue();

        hex834Instance0.Equals(hex834Instance1.Bytes).Should().BeTrue();
        hex834Instance0.Equals(hex834Instance2.Bytes).Should().BeTrue();
        hex834Instance0.Equals(hex834Instance3.Bytes).Should().BeTrue();
        hex834Instance0.Equals(hex834Instance4.Bytes).Should().BeTrue();
        hex834Instance0.Equals(hex834Instance5.Bytes).Should().BeTrue();
        hex834Instance0.Equals(hex834Instance6.Bytes).Should().BeTrue();
        hex834Instance0.Equals(hex834Instance7.Bytes).Should().BeTrue();
        hex834Instance0.Equals(hex834Instance8.Bytes).Should().BeTrue();
        hex834Instance0.Equals(hex834Instance9.Bytes).Should().BeTrue();
        hex834Instance0.Equals(hex834InstanceA.Bytes).Should().BeTrue();
        hex834Instance0.Equals(hex834InstanceB.Bytes).Should().BeTrue();
        hex834Instance0.Equals(hex834InstanceC.Bytes).Should().BeTrue();
        hex834Instance0.Equals(hex834InstanceD.Bytes).Should().BeTrue();
        hex834Instance0.Equals(hex834InstanceE.Bytes).Should().BeTrue();
        hex834Instance0.Equals(hex834InstanceF.Bytes).Should().BeTrue();

        Hex hex835Instance = 835;

        (hex834Instance0 == hex835Instance).Should().BeFalse();
        (hex834Instance0 != hex835Instance).Should().BeTrue();
        hex834Instance0.Equals(hex835Instance).Should().BeFalse();
        hex834Instance0.Integer.Equals(hex835Instance).Should().BeFalse();
        (hex834Instance0.Integer == hex835Instance.Integer).Should().BeFalse();
        (hex834Instance0.Integer != hex835Instance.Integer).Should().BeTrue();
        hex834Instance0.Integer.Equals(hex835Instance.Integer).Should().BeFalse();
        hex834Instance0.Equals(hex835Instance.Integer).Should().BeFalse();
        (hex834Instance0.HexString == hex835Instance.HexString).Should().BeFalse();
        (hex834Instance0.HexString != hex835Instance.HexString).Should().BeTrue();
        hex834Instance0.HexString.Equals(hex835Instance.HexString).Should().BeFalse();
    }

    [Fact]
    public void ToString_Works()
    {
        var bighex = new Hex(BigInteger.Parse("834772059474377393"));
        bighex.ToString().Should().Be("0xb95b4c3e9643ab1");
        bighex.ToString("N").Should().Be("834,772,059,474,377,393.00");
        bighex.ToString("X").Should().Be("0B95B4C3E9643AB1"); // Note the leading zero
        bighex.ToString("x").Should().Be("0b95b4c3e9643ab1"); // Note the leading zero
        bighex.ToString("D").Should().Be("834772059474377393");
        bighex.ToString("F6").Should().Be("834772059474377393.000000");
        bighex.ToString("C", new CultureInfo("en-US")).Should().Be("$834,772,059,474,377,393.00");
    }

    [Fact]
    public void Zero_Works()
    {
        Hex zeroHex = 0;
        zeroHex.Integer.Should().Be(0);
        zeroHex.HexString.Should().Be("0x0");
        zeroHex.Bytes.Should().BeEquivalentTo([0]);

        Hex zeroHex2 = "0x0";
        zeroHex2.Integer.Should().Be(0);
        zeroHex2.HexString.Should().Be("0x0");
        zeroHex2.Bytes.Should().BeEquivalentTo([0]);

        Hex zeroHex3 = "0";
        zeroHex3.Integer.Should().Be(0);
        zeroHex3.HexString.Should().Be("0x0");
        zeroHex3.Bytes.Should().BeEquivalentTo([0]);
    }

    [Fact]
    public void BigNumbers_Work()
    {
        Hex bigHex0 = "0xdac17f958d2ee523a2206206994597c13d831ec7";
        bigHex0.Integer.ToString().Should().Be("1248875146012964071876423320777688075155124985543");
        bigHex0.Bytes.Should().BeEquivalentTo(
        [218, 193, 127, 149, 141, 46, 229, 35, 162, 32, 98, 6, 153, 69, 151, 193, 61, 131, 30, 199]);

        Hex bigHex1 = "0x000000000000000000000000000000000000000000000001";
        bigHex1.Integer.ToString().Should().Be("1");
    }

    [Fact]
    public void BigNumberConversions_Work()
    {
        string bigHexString = "0xdac17f958d2ee523a2206206994597c13d831ec7";
        BigInteger intFromString = HexConvert.StringToInt(bigHexString);
        byte[] bytesFromString = HexConvert.StringToBytes(bigHexString);

        intFromString.ToString().Should().Be("1248875146012964071876423320777688075155124985543");
        bytesFromString.Should().BeEquivalentTo(
        [218, 193, 127, 149, 141, 46, 229, 35, 162, 32, 98, 6, 153, 69, 151, 193, 61, 131, 30, 199]);
    }

    [Fact]
    public void Conversions_Work()
    {
        void AssertConvertString(string value, BigInteger expectedInt, byte[] expectedBytes,
            byte[] expectedZeroTrimmedBytes)
        {
            BigInteger intFromString = HexConvert.StringToInt(value);
            intFromString.Should().Be(expectedInt);
            byte[] bytesFromString = HexConvert.StringToBytes(value);
            bytesFromString.Should().BeEquivalentTo(expectedBytes);
            byte[] bytesFromStringWithTrimZero = HexConvert.StringToBytes(value, true);
            bytesFromStringWithTrimZero.Should().BeEquivalentTo(expectedZeroTrimmedBytes);
        }

        void AssertConvertInteger(BigInteger value, string expectedString, byte[] expectedBytes)
        {
            string stringFromInt = HexConvert.IntToString(value);
            stringFromInt.Should().Be(expectedString);
            string stringFromIntWithoutPrefix = HexConvert.IntToString(value, false);
            stringFromIntWithoutPrefix.Should().Be(expectedString[2..]);
            byte[] bytesFromInt = HexConvert.IntToBytes(value);
            bytesFromInt.Should().BeEquivalentTo(expectedBytes);
        }

        void AssertConvertBytes(byte[] value, string expectedString, string expectedZeroTrimmedString,
            BigInteger expectedInt)
        {
            string stringFromBytes = HexConvert.BytesToString(value);
            stringFromBytes.Should().Be(expectedString);
            string zeroStringFromBytesWithoutPrefix = HexConvert.BytesToString(value, false);
            zeroStringFromBytesWithoutPrefix.Should().Be(expectedString[2..]);
            string zeroTrimmedStringFromBytes = HexConvert.BytesToString(value, trimZero: true);
            zeroTrimmedStringFromBytes.Should().Be(expectedZeroTrimmedString);
            string zeroTrimmedStringFromBytesWithoutPrefix = HexConvert.BytesToString(value, false, true);
            zeroTrimmedStringFromBytesWithoutPrefix.Should().Be(expectedZeroTrimmedString[2..]);
            BigInteger intFromBytes = HexConvert.BytesToInt(value);
            intFromBytes.Should().Be(expectedInt);
        }

        AssertConvertString("0x0", 0, [0], [0]);
        AssertConvertString("0x0000", 0, [0, 0], [0]);
        AssertConvertString("0x00000", 0, [0, 0, 0], [0]);
        AssertConvertString("0", 0, [0], [0]);
        AssertConvertInteger(0, "0x0", [0]);
        AssertConvertBytes([0], "0x00", "0x0", 0);
        AssertConvertBytes([0, 0], "0x0000", "0x0", 0);
        AssertConvertBytes([0, 0, 0], "0x000000", "0x0", 0);

        AssertConvertString("0x14", 20, [20], [20]);
        AssertConvertString("0x0014", 20, [0, 20], [20]);
        AssertConvertString("0x00014", 20, [0, 0, 20], [20]);
        AssertConvertInteger(20, "0x14", [20]);
        AssertConvertBytes([20], "0x14", "0x14", 20);
        AssertConvertBytes([0, 20], "0x0014", "0x14", 20);

        AssertConvertString("0xdac17f958d2ee523a2206206994597c13d831ec7",
            BigInteger.Parse("1248875146012964071876423320777688075155124985543"),
            [218, 193, 127, 149, 141, 46, 229, 35, 162, 32, 98, 6, 153, 69, 151, 193, 61, 131, 30, 199],
            [218, 193, 127, 149, 141, 46, 229, 35, 162, 32, 98, 6, 153, 69, 151, 193, 61, 131, 30, 199]);
        AssertConvertInteger(BigInteger.Parse("1248875146012964071876423320777688075155124985543"),
            "0xdac17f958d2ee523a2206206994597c13d831ec7",
            [218, 193, 127, 149, 141, 46, 229, 35, 162, 32, 98, 6, 153, 69, 151, 193, 61, 131, 30, 199]);
        AssertConvertBytes([218, 193, 127, 149, 141, 46, 229, 35, 162, 32, 98, 6, 153, 69, 151, 193, 61, 131, 30, 199],
            "0xdac17f958d2ee523a2206206994597c13d831ec7",
            "0xdac17f958d2ee523a2206206994597c13d831ec7",
            BigInteger.Parse("1248875146012964071876423320777688075155124985543"));

        AssertConvertString("0x00dac17f958d2ee523a2206206994597c13d831ec",
            BigInteger.Parse("78054696625810254492276457548605504697195311596"),
            [0, 13, 172, 23, 249, 88, 210, 238, 82, 58, 34, 6, 32, 105, 148, 89, 124, 19, 216, 49, 236],
            [13, 172, 23, 249, 88, 210, 238, 82, 58, 34, 6, 32, 105, 148, 89, 124, 19, 216, 49, 236]);
        AssertConvertInteger(BigInteger.Parse("78054696625810254492276457548605504697195311596"),
            "0xdac17f958d2ee523a2206206994597c13d831ec",
            [13, 172, 23, 249, 88, 210, 238, 82, 58, 34, 6, 32, 105, 148, 89, 124, 19, 216, 49, 236]);
        AssertConvertBytes([0, 13, 172, 23, 249, 88, 210, 238, 82, 58, 34, 6, 32, 105, 148, 89, 124, 19, 216, 49, 236],
            "0x000dac17f958d2ee523a2206206994597c13d831ec",
            "0xdac17f958d2ee523a2206206994597c13d831ec",
            BigInteger.Parse("78054696625810254492276457548605504697195311596"));
    }

    [Fact]
    public void Utils_Work()
    {
        const string hex0 = "0xc7";
        var hex0LeadingZerosRemoved = HexUtils.RemoveLeadingZeros(hex0);
        hex0LeadingZerosRemoved.Should().Be("0xc7");

        const string hex1 = "0x0c7";
        var hex1LeadingZerosRemoved = HexUtils.RemoveLeadingZeros(hex1);
        hex1LeadingZerosRemoved.Should().Be("0xc7");

        const string hex2 = "0x00c7";
        var hex2LeadingZerosRemoved = HexUtils.RemoveLeadingZeros(hex2);
        hex2LeadingZerosRemoved.Should().Be("0xc7");

        const string hex3 = "0x0";
        var hex3LeadingZerosRemoved = HexUtils.RemoveLeadingZeros(hex3);
        hex3LeadingZerosRemoved.Should().Be("0x0");

        const string hex4 = "0x00";
        var hex4LeadingZerosRemoved = HexUtils.RemoveLeadingZeros(hex4);
        hex4LeadingZerosRemoved.Should().Be("0x0");

        const string hex5 = "0x00c700";
        var hex5LeadingZerosRemoved = HexUtils.RemoveLeadingZeros(hex5);
        hex5LeadingZerosRemoved.Should().Be("0xc700");

        const string hex6 = "c7";
        var hex6LeadingZerosRemoved = HexUtils.RemoveLeadingZeros(hex6);
        hex6LeadingZerosRemoved.Should().Be("c7");

        const string hex7 = "00c7";
        var hex7LeadingZerosRemoved = HexUtils.RemoveLeadingZeros(hex7);
        hex7LeadingZerosRemoved.Should().Be("c7");

        var hex0Length4 = HexUtils.AppendLeadingZeros(hex0, 4);
        hex0Length4.Should().Be("0x00c7");

        var hex1Length4 = HexUtils.AppendLeadingZeros(hex1, 4);
        hex1Length4.Should().Be("0x00c7");

        var hex2Length4 = HexUtils.AppendLeadingZeros(hex2, 4);
        hex2Length4.Should().Be("0x00c7");

        var hex3Length4 = HexUtils.AppendLeadingZeros(hex3, 4);
        hex3Length4.Should().Be("0x0000");

        var hex4Length4 = HexUtils.AppendLeadingZeros(hex4, 4);
        hex4Length4.Should().Be("0x0000");

        var hex5Length4 = HexUtils.AppendLeadingZeros(hex5, 4);
        hex5Length4.Should().Be("0x00c700");

        var hex6Length4 = HexUtils.AppendLeadingZeros(hex6, 4);
        hex6Length4.Should().Be("00c7");

        var hex7Length4 = HexUtils.AppendLeadingZeros(hex7, 4);
        hex7Length4.Should().Be("00c7");

        var hex0Length40 = HexUtils.AppendLeadingZeros(hex0, 40);
        hex0Length40.Should().Be("0x00000000000000000000000000000000000000c7");

        var hex6Length40 = HexUtils.AppendLeadingZeros(hex6, 40);
        hex6Length40.Should().Be("00000000000000000000000000000000000000c7");

        var hex0SetLength3 = HexUtils.SetLength(hex0, 3);
        hex0SetLength3.Should().Be("0x0c7");

        var hex1SetLength3 = HexUtils.SetLength(hex1, 3);
        hex1SetLength3.Should().Be("0x0c7");

        var hex2SetLength3 = HexUtils.SetLength(hex2, 3);
        hex2SetLength3.Should().Be("0x0c7");

        var hex3SetLength3 = HexUtils.SetLength(hex3, 3);
        hex3SetLength3.Should().Be("0x000");

        var hex4SetLength3 = HexUtils.SetLength(hex4, 3);
        hex4SetLength3.Should().Be("0x000");

        var hex5SetLength3 = HexUtils.SetLength(hex5, 3);
        hex5SetLength3.Should().Be("0x700");

        var hex6SetLength3 = HexUtils.SetLength(hex6, 3);
        hex6SetLength3.Should().Be("0c7");

        var hex7SetLength3 = HexUtils.SetLength(hex7, 3);
        hex7SetLength3.Should().Be("0c7");

        var hex0SetLength40 = HexUtils.SetLength(hex0, 40);
        hex0SetLength40.Should().Be("0x00000000000000000000000000000000000000c7");

        var hex6SetLength40 = HexUtils.SetLength(hex6, 40);
        hex6SetLength40.Should().Be("00000000000000000000000000000000000000c7");

        const string hex8 = "0000000000000000000000ff1a85eb6d4c45d025634ea7a07d8818aedd3b9d8d";
        var hex8SetLength40 = HexUtils.SetLength(hex8, 40);
        hex8SetLength40.Should().Be("1a85eb6d4c45d025634ea7a07d8818aedd3b9d8d");
    }

    [Fact]
    public void InvalidInitializationTests()
    {
        Action action0 = () => "".ToHex();
        action0.Should().Throw<ArgumentException>();

        Action action1 = () => "123asd".ToHex();
        action1.Should().Throw<ArgumentException>();

        Action action2 = () => "0x0x0".ToHex();
        action2.Should().Throw<ArgumentException>();

        Action action3 = () => "0.001".ToHex();
        action3.Should().Throw<ArgumentException>();

        Action action4 = () => "0x".ToHex();
        action4.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void EdgeCaseTests()
    {
        ("0x0".ToHex().Integer == 0).Should().BeTrue();
        ("0x000000000000000".ToHex().Integer == 0).Should().BeTrue();
    }

    [Fact]
    public void EmptyHexString_ShouldBeHandledAsZero()
    {
        // After fixing the code, this test should pass
        // "0x" should be interpreted as 0
        var zeroXHex = "0x".ToHex();
        zeroXHex.Integer.Should().Be(0);
        zeroXHex.Bytes.Should().BeEquivalentTo([0]);
    }
}