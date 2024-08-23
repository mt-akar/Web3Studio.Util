namespace Web3Studio.Util.Tests;

public sealed class JsonRpcModelTests
{
    [Theory]
    [InlineData("eth_someMethod", new object[] {1, "valid", true}, null,
        """{"jsonrpc":"2.0","method":"eth_someMethod","params":[1,"valid",true],"id":1}""")]
    [InlineData("eth_someMethod", new object[] {"0xdac17f958d2ee523a2206206994597c13d831ec7", "latest"}, 1,
        """{"jsonrpc":"2.0","method":"eth_someMethod","params":["0xdac17f958d2ee523a2206206994597c13d831ec7","latest"],"id":1}""")]
    [InlineData("eth_someMethod", new object[] { }, 2,
        """{"jsonrpc":"2.0","method":"eth_someMethod","params":[],"id":2}""")]
    public void JsonRpcRequestData_JsonConversion_Works(string method, object parameters, object? id, string expectedJson)
    {
        // Arrange
        var request = new JsonRpcRequestData(method, parameters, id);
        
        // Act
        var json = request.ToJson();
        
        // Assert
        json.Should().Be(expectedJson);
    }

    [Fact]
    public void JsonToResult_StringResponse_Works()
    {
        // Arrange
        const string response = """{"jsonrpc":"2.0","id":1,"result":"0x139ffe2"}""";

        // Act
        var result = JsonRpcConvert.JsonToResult<string>(response);
        
        // Assert
        result.IsSuccess.Should().Be(true);
        result.Response.Should().Be("0x139ffe2");
        result.Error.Should().BeNull();
    }

    private record SyncingResult(string StartingBlock, string CurrentBlock, string HighestBlock);

    [Fact]
    public void JsonToResult_ObjectResponse_Works()
    {
        // Arrange
        const string response =
            """{"jsonrpc":"2.0","id": 1,"result":{"startingBlock":"0x384","currentBlock":"0x386","highestBlock":"0x454"}}""";

        // Act
        var result = JsonRpcConvert.JsonToResult<SyncingResult>(response);
        
        // Assert
        result.IsSuccess.Should().Be(true);
        result.Error.Should().BeNull();
        result.Response.StartingBlock.Should().Be("0x384");
        result.Response.CurrentBlock.Should().Be("0x386");
        result.Response.HighestBlock.Should().Be("0x454");
    }

    [Fact]
    public void JsonToResult_Error_Works()
    {
        // Arrange
        const string response =
            """{"jsonrpc":"2.0","id":1,"error":{"code":-32601,"message":"the method eth_blockNumbe does not exist/is not available"}}""";

        // Act
        var result = JsonRpcConvert.JsonToResult<string>(response);
        
        // Assert
        result.IsSuccess.Should().Be(false);
        result.Response.Should().BeNull();
        result.Error.Should().BeEquivalentTo(new
        {
            Code = -32601,
            Message = "the method eth_blockNumbe does not exist/is not available",
        });
    }

    [Fact]
    public void JsonToResult_ParseError_Returns32700ParseError()
    {
        // Arrange
        const string response = """{"jsonrpc":"2.0","id":1,"response":""";

        // Act
        var result = JsonRpcConvert.JsonToResult<string>(response);
        
        // Assert
        result.IsSuccess.Should().Be(false);
        result.Response.Should().BeNull();
        result.Error.Should().BeEquivalentTo(new
        {
            Code = -32700,
            Message = "Parse error",
        });
    }
}