using System.Web;
using System.Web.Http;
using Lab2.Models;

namespace Lab2.Controllers
{
    public class RamController : ApiController
    {
        private static RamModel AppRamStorage =>
            HttpContext.Current.Application[WebApiApplication.AppRamStorageName] as RamModel;

        public IHttpActionResult Get()
        {
            var result = AppRamStorage.Result;
            if (AppRamStorage.Stack.Count != 0)
            {
                result += AppRamStorage.Stack.Peek();
            }
            return Ok(new { result });
        }

        public IHttpActionResult Post([FromUri] string result)
        {
            if (!int.TryParse(result, out int resultValue))
            {
                return BadRequest("Parameter 'result' is invalid.");
            }
            AppRamStorage.Result = resultValue;
            return Ok();
        }

        public IHttpActionResult Put([FromUri] string add)
        {
            if (!int.TryParse(add, out int addValue))
            {
                return BadRequest("Parameter 'add' is invalid.");
            }
            AppRamStorage.Stack.Push(addValue);
            return Ok();
        }

        public IHttpActionResult Delete()
        {
            if (AppRamStorage.Stack.Count == 0)
            {
                return BadRequest("Stack is empty.");
            }
            AppRamStorage.Stack.Pop();
            return Ok();
        }
    }
}
