using System.ComponentModel;

namespace JRService.Models
{
    public enum ErrorCode
    {
        [Description("Parse error")]
        ParseError = -32700,

        [Description("Invalid request")]
        InvalidRequest = -32600,

        [Description("Method not found")]
        MethodNotFound = -32601,

        [Description("Invalid params")]
        InvalidParams = -32602,

        [Description("Internal error")]
        InternalError = -32603,

        //==========================================

        [Description("JRService: Key not found")]
        JRS_KeyNotFound = -32000,

        [Description("JRService: Division by zero")]
        JRS_DivisionByZero = -32001,
    }

    public static class ErrorCodeExtensions
    {
        public static string GetMessage(this ErrorCode errorCode)
        {
            var type = typeof(ErrorCode);
            var memberInfo = type.GetMember(errorCode.ToString());
            var attributes = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes.Length > 0
                ? ((DescriptionAttribute)attributes[0]).Description
                : "Unknown error.";
        }
    }
}
