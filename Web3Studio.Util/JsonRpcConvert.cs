using System;
using System.Text.Json;

namespace Web3Studio.Util
{
    public static class JsonRpcConvert
    {
        public static readonly JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions
        {
            MaxDepth = 65536,
            PropertyNameCaseInsensitive = true,
            Converters = {new HexJsonConverter()},
        };

        public static JsonRpcResult<TResult> JsonToResult<TResult>(string json)
        {
            try
            {
                var response = JsonSerializer.Deserialize<JsonRpcResponse<TResult>>(json, JsonSerializerOptions);
                if (response == null)
                    return new JsonRpcResponseError {Code = -32700, Message = "Parse error"};
                if (response.Error != null)
                    return response.Error;

                return response.Result!;
            }
            catch (JsonException)
            {
                return new JsonRpcResponseError {Code = -32700, Message = "Parse error"};
            }
        }
    }

    public class JsonRpcException : Exception
    {
        public JsonRpcException(string message) : base(message)
        {
        }
    }
}