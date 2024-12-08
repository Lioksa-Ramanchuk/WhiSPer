using Newtonsoft.Json;

namespace JRService.Models
{
    public class JsonRpcError
    {
        public JsonRpcError(int code, string message, object data = null)
        {
            Code = code;
            Message = message;
            Data = data;
        }

        public JsonRpcError(ErrorCode code, object data = null)
        {
            Code = (int)code;
            Message = code.GetMessage();
            Data = data;
        }

        [JsonProperty("code")]
        public int Code { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("data")]
        public object Data { get; set; }
    }
}
