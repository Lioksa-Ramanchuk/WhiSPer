using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;
using Lab1.Models;

namespace Lab1.Handlers
{
    public class RamHandler : IHttpHandler, IRequiresSessionState
    {
        private static readonly string SessionRamStorageName = "Ram";

        private static int _result = 0;

        private readonly JavaScriptSerializer _jsSerializer = new JavaScriptSerializer();

        public bool IsReusable => false;

        public void ProcessRequest(HttpContext context)
        {
            switch (context.Request.HttpMethod)
            {
                case "GET":
                    HandleGet(context);
                    break;
                case "POST":
                    HandlePost(context);
                    break;
                case "PUT":
                    HandlePut(context);
                    break;
                case "DELETE":
                    HandleDelete(context);
                    break;
                default:
                    context.Response.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
                    context.Response.Write("Method Not Allowed");
                    break;
            }
        }

        private void HandleGet(HttpContext context)
        {
            var ramModel =
                GetObjectFromSession<RamModel>(context, _jsSerializer, SessionRamStorageName)
                ?? new RamModel();
            int result = _result;
            if (ramModel.StackCount != 0)
            {
                result += ramModel.StackPeek();
            }

            context.Response.StatusCode = (int)HttpStatusCode.OK;
            context.Response.ContentType = "application/json";
            context.Response.Write(_jsSerializer.Serialize(new { Result = result }));
        }

        private void HandlePost(HttpContext context)
        {
            if (!int.TryParse(context.Request.QueryString["result"], out int result))
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";
                context.Response.Write("{\"Message\":\"Parameter 'result' is invalid.\"}");
                return;
            }
            _result = result;
            context.Response.StatusCode = (int)HttpStatusCode.OK;
        }

        private void HandlePut(HttpContext context)
        {
            var ramModel =
                GetObjectFromSession<RamModel>(context, _jsSerializer, SessionRamStorageName)
                ?? new RamModel();
            if (!int.TryParse(context.Request.QueryString["add"], out int add))
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";
                context.Response.Write("{\"Message\":\"Parameter 'add' is invalid.\"}");
                return;
            }
            ramModel.StackPush(add);
            SetObjectIntoSession(context, _jsSerializer, SessionRamStorageName, ramModel);
            context.Response.StatusCode = (int)HttpStatusCode.OK;
        }

        private void HandleDelete(HttpContext context)
        {
            var ramModel =
                GetObjectFromSession<RamModel>(context, _jsSerializer, SessionRamStorageName)
                ?? new RamModel();
            if (ramModel.StackCount == 0)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";
                context.Response.Write("{\"Message\":\"Stack is empty.\"}");
                return;
            }
            ramModel.StackPop();
            SetObjectIntoSession(context, _jsSerializer, SessionRamStorageName, ramModel);
            context.Response.StatusCode = (int)HttpStatusCode.OK;
        }

        private static T GetObjectFromSession<T>(
            HttpContext context,
            JavaScriptSerializer jsSerializer,
            string name
        )
        {
            var json = context.Session[name] as string;
            return string.IsNullOrEmpty(json) ? default : jsSerializer.Deserialize<T>(json);
        }

        private static void SetObjectIntoSession<T>(
            HttpContext context,
            JavaScriptSerializer jsSerializer,
            string name,
            T o
        )
        {
            context.Session[name] = jsSerializer.Serialize(o);
        }
    }
}
