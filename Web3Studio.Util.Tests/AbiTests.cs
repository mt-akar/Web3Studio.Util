namespace Web3Studio.Util.Tests;

public sealed class AbiTests
{
    [Fact]
    public void Decoder_Works()
    {
        var meaningOfUniverse = AbiEncoder.DecodeToHex(
            "0x000000000000000000000000000000000000000000000000000000000000002a");
        meaningOfUniverse.Should().Be(42);

        var usdtName = AbiEncoder.DecodeString(
            "0x" +
            "0000000000000000000000000000000000000000000000000000000000000020" +
            "000000000000000000000000000000000000000000000000000000000000000a" +
            "5465746865722055534400000000000000000000000000000000000000000000");
        usdtName.Should().Be("Tether USD");

        var usdtSymbol = AbiEncoder.DecodeToValueTuple<ValueTuple<string>>(
            "0x" +
            "0000000000000000000000000000000000000000000000000000000000000020" +
            "0000000000000000000000000000000000000000000000000000000000000004" +
            "5553445400000000000000000000000000000000000000000000000000000000");
        usdtSymbol.Item1.Should().Be("USDT");

        var (symbol, name) = AbiEncoder.DecodeToValueTuple<(string, string)>(
            "0x" +
            "0000000000000000000000000000000000000000000000000000000000000040" +
            "0000000000000000000000000000000000000000000000000000000000000080" +
            "0000000000000000000000000000000000000000000000000000000000000004" +
            "5553445400000000000000000000000000000000000000000000000000000000" +
            "000000000000000000000000000000000000000000000000000000000000000a" +
            "5465746865722055534400000000000000000000000000000000000000000000");
        symbol.Should().Be("USDT");
        name.Should().Be("Tether USD");

        var (item00, item01, item02) = AbiEncoder.DecodeToValueTuple<(Hex, Hex, Hex)>(
            "0x" +
            "0000000000000000000000007cb57b5a97eabe94205c07890be4c1ad31e486a8" +
            "000000000000000000000000000000000000000000000000000000000000000a" +
            "0000000000000000000000007cb57b5a97eabe94205c07890be4c1ad31e486a9");
        item00.Should().Be("0x7cb57b5a97eabe94205c07890be4c1ad31e486a8");
        item01.Should().Be(10);
        item02.Should().Be("0x7cb57b5a97eabe94205c07890be4c1ad31e486a9");

        var (item10, item11, item12) = AbiEncoder.DecodeToValueTuple<(Hex, string, bool)>(
            "0x" +
            "0000000000000000000000000000000000000000000000000000000000000008" +
            "0000000000000000000000000000000000000000000000000000000000000060" +
            "0000000000000000000000000000000000000000000000000000000000000001" +
            "0000000000000000000000000000000000000000000000000000000000000016" +
            "436861696E204761746520697320617765736F6D652100000000000000000000");
        item10.Should().Be(8);
        item11.Should().Be("Chain Gate is awesome!");
        item12.Should().Be(true);

        // This works but it is pointless
        var valueTuple =
            AbiEncoder.DecodeToValueTuple<ValueTuple>(
                "0x0000000000000000000000000000000000000000000000000000000000000020");
    }

    [Fact]
    public void Decoder_HandlesZeros()
    {
        var (item10, item11, item12) = AbiEncoder.DecodeToValueTuple<(Hex, Hex, Hex)>(
            "0x" +
            "0000000000000000000000000000000000000000000000000000000000000004" +
            "0000000000000000000000000000000000000000000000000000000000000000" +
            "0000000000000000000000000000000000000000000000008ac7230489e80000");

        item10.Should().Be(4);
        item11.Should().Be(0);
        item12.Integer.Should().Be(10000000000000000000);
    }

    [Fact]
    public void Encoder_Works()
    {
        var meaningOfUniverse = AbiEncoder.Encode(ValueTuple.Create(42));
        meaningOfUniverse.Should().Be("0x000000000000000000000000000000000000000000000000000000000000002a");
        var meaningOfUniverse2 = AbiEncoder.EncodeObjects([42]);
        meaningOfUniverse2.Should().Be(meaningOfUniverse);

        var meaningOfUniverseWithMethodHash = AbiEncoder.Encode("1234abcd", ValueTuple.Create(42));
        meaningOfUniverseWithMethodHash.Should()
            .Be("0x1234abcd000000000000000000000000000000000000000000000000000000000000002a");
        var meaningOfUniverse2WithMethodHash = AbiEncoder.EncodeObjects("1234abcd", [42]);
        meaningOfUniverse2WithMethodHash.Should().Be(meaningOfUniverseWithMethodHash);

        var usdtName = AbiEncoder.Encode(ValueTuple.Create("Tether USD"));
        usdtName.Should()
            .Be("0x" +
                "0000000000000000000000000000000000000000000000000000000000000020" +
                "000000000000000000000000000000000000000000000000000000000000000a" +
                "5465746865722055534400000000000000000000000000000000000000000000");
        var usdtName2 = AbiEncoder.EncodeObjects(["Tether USD"]);
        usdtName2.Should().Be(usdtName);

        var usdtNameWithMethodHash = AbiEncoder.Encode("abcd1234", ValueTuple.Create("Tether USD"));
        usdtNameWithMethodHash.Should()
            .Be("0xabcd1234" +
                "0000000000000000000000000000000000000000000000000000000000000020" +
                "000000000000000000000000000000000000000000000000000000000000000a" +
                "5465746865722055534400000000000000000000000000000000000000000000");
        var usdtName2WithMethodHash = AbiEncoder.EncodeObjects("abcd1234", ["Tether USD"]);
        usdtName2WithMethodHash.Should().Be(usdtNameWithMethodHash);

        var numbers = new ValueTuple<Hex, Hex, Hex>(78, 42, 12);
        var encodedNumbers = AbiEncoder.Encode(numbers);
        encodedNumbers.Should()
            .Be("0x" +
                "000000000000000000000000000000000000000000000000000000000000004e" +
                "000000000000000000000000000000000000000000000000000000000000002a" +
                "000000000000000000000000000000000000000000000000000000000000000c");
        var (number1, number2, number3) = AbiEncoder.DecodeToValueTuple<(Hex, Hex, Hex)>(encodedNumbers);
        number1.Should().Be(78);
        number2.Should().Be(42);
        number3.Should().Be(12);

        var fullHexes = new ValueTuple<Hex, Hex>(
            "0x3d5b1ae49ef60a7411d2471a80c9830ff97baa952f2bb61689077f536260b147",
            "0x5eadf7e0a01d8b5c1b93f4fe0805daca15982431324be89de4141714e4444bdd");
        var encodedFullHexes = AbiEncoder.Encode(fullHexes);
        encodedFullHexes.Should()
            .Be("0x" +
                "3d5b1ae49ef60a7411d2471a80c9830ff97baa952f2bb61689077f536260b147" +
                "5eadf7e0a01d8b5c1b93f4fe0805daca15982431324be89de4141714e4444bdd");
        var (fullHex0, fullHex1) = AbiEncoder.DecodeToValueTuple<(Hex, Hex)>(encodedFullHexes);
        fullHex0.Should().Be("0x3d5b1ae49ef60a7411d2471a80c9830ff97baa952f2bb61689077f536260b147");
        fullHex1.Should().Be("0x5eadf7e0a01d8b5c1b93f4fe0805daca15982431324be89de4141714e4444bdd");

        var encodedNumbersWithMethodHash = AbiEncoder.Encode("12345678", numbers);
        encodedNumbersWithMethodHash.Should()
            .Be("0x12345678" +
                "000000000000000000000000000000000000000000000000000000000000004e" +
                "000000000000000000000000000000000000000000000000000000000000002a" +
                "000000000000000000000000000000000000000000000000000000000000000c");

        var mixedInput = (5478.ToHex(), "Tether USD", true);
        var encodedMixedInput = AbiEncoder.Encode(mixedInput);
        encodedMixedInput.Should()
            .Be("0x" +
                "0000000000000000000000000000000000000000000000000000000000001566" +
                "0000000000000000000000000000000000000000000000000000000000000060" +
                "0000000000000000000000000000000000000000000000000000000000000001" +
                "000000000000000000000000000000000000000000000000000000000000000a" +
                "5465746865722055534400000000000000000000000000000000000000000000");
        var encodedMixedInput2 = AbiEncoder.EncodeObjects([5478.ToHex(), "Tether USD", true]);
        encodedMixedInput2.Should().Be(encodedMixedInput);
        var (number, name, flag) = AbiEncoder.DecodeToValueTuple<(Hex, string, bool)>(encodedMixedInput);
        number.Should().Be(5478);
        name.Should().Be("Tether USD");
        flag.Should().Be(true);

        var strings = ("Chain", "Gate", "is", "awesome!");
        var encodedStrings = AbiEncoder.Encode(strings);
        encodedStrings.Should()
            .Be("0x" +
                "0000000000000000000000000000000000000000000000000000000000000080" +
                "00000000000000000000000000000000000000000000000000000000000000c0" +
                "0000000000000000000000000000000000000000000000000000000000000100" +
                "0000000000000000000000000000000000000000000000000000000000000140" +
                "0000000000000000000000000000000000000000000000000000000000000005" +
                "436861696e000000000000000000000000000000000000000000000000000000" +
                "0000000000000000000000000000000000000000000000000000000000000004" +
                "4761746500000000000000000000000000000000000000000000000000000000" +
                "0000000000000000000000000000000000000000000000000000000000000002" +
                "6973000000000000000000000000000000000000000000000000000000000000" +
                "0000000000000000000000000000000000000000000000000000000000000008" +
                "617765736f6d6521000000000000000000000000000000000000000000000000");
        var encodedStrings2 = AbiEncoder.EncodeObjects(["Chain", "Gate", "is", "awesome!"]);
        encodedStrings2.Should().Be(encodedStrings);
        var (string1, string2, string3, string4) =
            AbiEncoder.DecodeToValueTuple<(string, string, string, string)>(encodedStrings);
        string1.Should().Be("Chain");
        string2.Should().Be("Gate");
        string3.Should().Be("is");
        string4.Should().Be("awesome!");

        var hexArray = ValueTuple.Create(new[] { 15.ToHex(), 30.ToHex(), 45.ToHex() });
        var encodedHexArray = AbiEncoder.Encode(hexArray);
        encodedHexArray.Should()
            .Be("0x" +
                "0000000000000000000000000000000000000000000000000000000000000020" +
                "0000000000000000000000000000000000000000000000000000000000000003" +
                "000000000000000000000000000000000000000000000000000000000000000f" +
                "000000000000000000000000000000000000000000000000000000000000001e" +
                "000000000000000000000000000000000000000000000000000000000000002d");

        var arraysAndValues = (new[] { 65, 75, 85 }, 420,
            new Hex[] { "7cb57b5a97eabe94205c07890be4c1ad31e486a8", "94205c07890be4c1ad31e486a87cb57b5a97eabe" });
        var encodedArraysAndValues = AbiEncoder.Encode(arraysAndValues);
        encodedArraysAndValues.Should()
            .Be("0x" +
                "0000000000000000000000000000000000000000000000000000000000000060" +
                "00000000000000000000000000000000000000000000000000000000000001a4" +
                "00000000000000000000000000000000000000000000000000000000000000e0" +
                "0000000000000000000000000000000000000000000000000000000000000003" +
                "0000000000000000000000000000000000000000000000000000000000000041" +
                "000000000000000000000000000000000000000000000000000000000000004b" +
                "0000000000000000000000000000000000000000000000000000000000000055" +
                "0000000000000000000000000000000000000000000000000000000000000002" +
                "0000000000000000000000007cb57b5a97eabe94205c07890be4c1ad31e486a8" +
                "00000000000000000000000094205c07890be4c1ad31e486a87cb57b5a97eabe");
    }
}