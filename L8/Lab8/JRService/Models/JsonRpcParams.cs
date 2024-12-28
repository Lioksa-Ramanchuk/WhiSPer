using Newtonsoft.Json;

namespace JRService.Models
{
    public abstract class JsonRpcParams
    {
        public abstract object[] ToArgs();
    }

    public class ParamsK : JsonRpcParams
    {
        [JsonProperty("k")]
        public string K { get; set; }

        public override object[] ToArgs() => new object[] { K };
    };

    public class ParamsKX : JsonRpcParams
    {
        [JsonProperty("k")]
        public string K { get; set; }

        [JsonProperty("x")]
        public int X { get; set; }

        public override object[] ToArgs() => new object[] { K, X };
    };
}
