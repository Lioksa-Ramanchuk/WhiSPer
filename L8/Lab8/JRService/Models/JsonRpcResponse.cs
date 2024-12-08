using Newtonsoft.Json;

namespace JRService.Models
{
    public class JsonRpcResponse
    {
        [JsonProperty("jsonrpc")]
        public string Jsonrpc { get; set; } = "2.0";

        [JsonProperty("result")]
        public object Result { get; set; }

        [JsonProperty("error")]
        public JsonRpcError Error { get; set; }

        [JsonProperty("id")]
        public object Id { get; set; }
    }
}
