using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using JRService.Models;
using Newtonsoft.Json.Linq;

namespace JRService.Controllers
{
    [Route("api/jrservice")]
    public class JRServiceController : ApiController
    {
        private const string SessionKey = "JrServiceStorage";
        private readonly Dictionary<string, MethodHandler> _methods;

        public JRServiceController()
            : base()
        {
            _methods = new Dictionary<string, MethodHandler>
            {
                {
                    nameof(SetM),
                    new MethodHandler(new Func<string, int, int>(SetM), typeof(ParamsKX))
                },
                { nameof(GetM), new MethodHandler(new Func<string, int>(GetM), typeof(ParamsK)) },
                {
                    nameof(AddM),
                    new MethodHandler(new Func<string, int, int>(AddM), typeof(ParamsKX))
                },
                {
                    nameof(SubM),
                    new MethodHandler(new Func<string, int, int>(SubM), typeof(ParamsKX))
                },
                {
                    nameof(MulM),
                    new MethodHandler(new Func<string, int, int>(MulM), typeof(ParamsKX))
                },
                {
                    nameof(DivM),
                    new MethodHandler(new Func<string, int, int>(DivM), typeof(ParamsKX))
                },
                { nameof(ErrorExit), new MethodHandler(new Action(ErrorExit), null) },
            };
        }

        private bool ErrorExitFlag { get; set; } = false;

        private Dictionary<string, int> SessionMemory
        {
            get
            {
                if (HttpContext.Current.Session[SessionKey] is null)
                {
                    HttpContext.Current.Session[SessionKey] = new Dictionary<string, int>();
                }
                return (Dictionary<string, int>)HttpContext.Current.Session[SessionKey];
            }
        }

        [HttpPost]
        public object ProcessJsonRpcRequest([FromBody] JToken requestToken)
        {
            try
            {
                if (requestToken.Type == JTokenType.Object)
                {
                    JsonRpcRequest singleRequest = null;
                    try
                    {
                        singleRequest = requestToken.ToObject<JsonRpcRequest>();
                    }
                    catch
                    {
                        return CreateErrorResponse(ErrorCode.InvalidRequest, null);
                    }

                    return ProcessRequest(singleRequest);
                }

                if (requestToken.Type == JTokenType.Array)
                {
                    JsonRpcRequest[] batchRequests = Array.Empty<JsonRpcRequest>();
                    try
                    {
                        batchRequests = requestToken.ToObject<JsonRpcRequest[]>();
                    }
                    catch
                    {
                        return CreateErrorResponse(ErrorCode.InvalidRequest, null);
                    }

                    var response = batchRequests
                        .TakeWhile(_ => !ErrorExitFlag)
                        .Select(ProcessRequest)
                        .ToArray();
                    ErrorExitFlag = false;
                    return response;
                }

                return CreateErrorResponse(ErrorCode.InvalidRequest, null);
            }
            catch (Exception)
            {
                return CreateErrorResponse(ErrorCode.InternalError, null);
            }
        }

        private JsonRpcResponse ProcessRequest(JsonRpcRequest request)
        {
            if (request.Id == null)
            {
                return CreateErrorResponse(ErrorCode.InvalidRequest, null);
            }

            if (request.Jsonrpc != "2.0")
            {
                return CreateErrorResponse(ErrorCode.InvalidRequest, request.Id);
            }

            if (!_methods.TryGetValue(request.Method, out var handler))
            {
                return CreateErrorResponse(ErrorCode.MethodNotFound, request.Id);
            }

            try
            {
                if (handler.ParamsType is null)
                {
                    return request.Params is null
                        ? CreateSuccessfulResponse(handler.Invoke(), request.Id)
                        : CreateErrorResponse(ErrorCode.InvalidParams, request.Id);
                }

                JsonRpcParams jrpcParams;
                try
                {
                    jrpcParams =
                        (request.Params as JObject)?.ToObject(handler.ParamsType) as JsonRpcParams;
                }
                catch (Exception)
                {
                    return CreateErrorResponse(ErrorCode.InvalidParams, request.Id);
                }

                return jrpcParams is null
                    ? CreateErrorResponse(ErrorCode.InvalidParams, request.Id)
                    : CreateSuccessfulResponse(handler.Invoke(jrpcParams), request.Id);
            }
            catch (Exception ex) when (ex.InnerException is JRServiceException jrsException)
            {
                return CreateErrorResponse(jrsException.Code, request.Id);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                return CreateErrorResponse(ErrorCode.InternalError, request.Id);
            }
        }

        private JsonRpcResponse CreateSuccessfulResponse(object result, object id)
        {
            return new JsonRpcResponse { Result = result, Id = id };
        }

        private JsonRpcResponse CreateErrorResponse(ErrorCode code, object id)
        {
            return new JsonRpcResponse { Error = new JsonRpcError(code), Id = id };
        }

        private int SetM(string k, int x)
        {
            SessionMemory[k] = x;
            return SessionMemory[k];
        }

        private int GetM(string k)
        {
            if (!SessionMemory.TryGetValue(k, out int x))
            {
                throw new JRServiceException(ErrorCode.JRS_KeyNotFound);
            }

            return x;
        }

        private int AddM(string k, int x)
        {
            if (!SessionMemory.ContainsKey(k))
            {
                throw new JRServiceException(ErrorCode.JRS_KeyNotFound);
            }

            SessionMemory[k] += x;
            return SessionMemory[k];
        }

        private int SubM(string k, int x)
        {
            if (!SessionMemory.ContainsKey(k))
            {
                throw new JRServiceException(ErrorCode.JRS_KeyNotFound);
            }

            SessionMemory[k] -= x;
            return SessionMemory[k];
        }

        private int MulM(string k, int x)
        {
            if (!SessionMemory.ContainsKey(k))
            {
                throw new JRServiceException(ErrorCode.JRS_KeyNotFound);
            }

            SessionMemory[k] *= x;
            return SessionMemory[k];
        }

        private int DivM(string k, int x)
        {
            if (!SessionMemory.ContainsKey(k))
            {
                throw new JRServiceException(ErrorCode.JRS_KeyNotFound);
            }

            if (x == 0)
            {
                throw new JRServiceException(ErrorCode.JRS_DivisionByZero);
            }

            SessionMemory[k] /= x;
            return SessionMemory[k];
        }

        private void ErrorExit()
        {
            ErrorExitFlag = true;
            HttpContext.Current.Session.Clear();
        }
    }
}
