using System;

namespace JRService.Models
{
    public class MethodHandler
    {
        public Delegate Method { get; }
        public Type ParamsType { get; }

        public MethodHandler(Delegate method, Type paramsType)
        {
            Method = method;
            ParamsType = paramsType;
        }

        public object Invoke(JsonRpcParams parameters = null)
        {
            return Method.DynamicInvoke(parameters?.ToArgs() ?? Array.Empty<object>());
        }
    }
}
