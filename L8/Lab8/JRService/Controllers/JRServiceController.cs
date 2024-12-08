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
                    var singleRequest = requestToken.ToObject<JsonRpcRequest>();
                    return ProcessRequest(singleRequest);
                }

                if (requestToken.Type == JTokenType.Array)
                {
                    var batchRequests = requestToken.ToObject<JsonRpcRequest[]>();
                    var response = batchRequests
                        .TakeWhile(_ => !ErrorExitFlag)
                        .Select(ProcessRequest)
                        .ToArray();
                    ErrorExitFlag = false;
                    return response;
                }

                return new JsonRpcResponse
                {
                    Error = new JsonRpcError(ErrorCode.InvalidRequest),
                    Id = null,
                };
            }
            catch (Exception)
            {
                return new JsonRpcResponse
                {
                    Error = new JsonRpcError(ErrorCode.InternalError),
                    Id = null,
                };
            }
        }

        private JsonRpcResponse ProcessRequest(JsonRpcRequest request)
        {
            if (request.Jsonrpc != "2.0" || request.Id == null)
            {
                return CreateErrorResponse(ErrorCode.InvalidRequest);
            }

            if (!_methods.TryGetValue(request.Method, out var handler))
            {
                return CreateErrorResponse(ErrorCode.MethodNotFound);
            }

            try
            {
                if (handler.ParamsType is null)
                {
                    return request.Params is null
                        ? CreateSuccessfulResponse(handler.Invoke(), request.Id)
                        : CreateErrorResponse(ErrorCode.InvalidParams);
                }

                JsonRpcParams jrpcParams;
                try
                {
                    jrpcParams = (request.Params as JObject)?.ToObject(handler.ParamsType) as JsonRpcParams;
                }
                catch (Exception)
                {
                    return CreateErrorResponse(ErrorCode.InvalidParams);
                }

                return jrpcParams is null
                    ? CreateErrorResponse(ErrorCode.InvalidParams)
                    : CreateSuccessfulResponse(handler.Invoke(jrpcParams), request.Id);
            }
            catch (Exception ex) when (ex.InnerException is JRServiceException jrsException)
            {
                return new JsonRpcResponse
                {
                    Error = new JsonRpcError(jrsException.Code),
                    Id = null,
                };
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);

                return new JsonRpcResponse
                {
                    Error = new JsonRpcError(ErrorCode.InternalError),
                    Id = null,
                };
            }
        }

        private JsonRpcResponse CreateSuccessfulResponse(object result, object id)
        {
            return new JsonRpcResponse { Result = result, Id = id };
        }

        private JsonRpcResponse CreateErrorResponse(ErrorCode code)
        {
            return new JsonRpcResponse { Error = new JsonRpcError(code), Id = null };
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
