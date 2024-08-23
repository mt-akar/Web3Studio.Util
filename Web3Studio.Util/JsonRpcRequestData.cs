using System.Text.Json;
using System.Text.Json.Serialization;

namespace Web3Studio.Util
{
    public class JsonRpcRequestData
    {
        [JsonPropertyName("jsonrpc")]
        public string JsonRpc { get; set; } = "2.0";

        [JsonPropertyName("method")]
        public string Method { get; set; }

        [JsonPropertyName("params")]
        public object Params { get; set; }

        [JsonPropertyName("id")]
        public object Id { get; set; } = 1;
        
        /// <summary>
        /// Default constructor for JSON deserialization
        /// </summary>
        public JsonRpcRequestData()
        {
        }

        public JsonRpcRequestData(string method, object @params, object? id = null)
        {
            Method = method;
            Params = @params;
            Id = id ?? 1;
        }

        public string ToJson()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = false,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            };
            return JsonSerializer.Serialize(this, options);
        }
    }

    public class JsonRpcResponse<TResult>
    {
        [JsonPropertyName ("jsonrpc")]
        public string JsonRpc { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("result")]
        public TResult Result { get; set; }

        [JsonPropertyName("error")]
        public JsonRpcResponseError? Error { get; set; }

        public string ToJson()
        {
            var options = new JsonSerializerOptions {
                WriteIndented = false,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            };
            return JsonSerializer.Serialize(this, options);
        }
    }

    public class JsonRpcResponseError
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }
        
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}