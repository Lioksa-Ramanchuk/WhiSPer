using System.IO;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using Lab4.Models;

namespace Simplex
{
    /// <summary>
    /// ASMX web service Simplex
    /// </summary>
    [WebService(Namespace = "http://RAM/", Description = "ASMX web service Simplex")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [ScriptService]
    public class Simplex : WebService
    {
        [WebMethod(Description = "Returns the sum of two integers", MessageName = "Add")]
        public int Add(int x, int y) => x + y;

        [WebMethod(
            Description = "Returns the concatenation of a string and a double",
            MessageName = "Concat"
        )]
        public string Concat(string s, double d) => s + d;

        [WebMethod(Description = "Returns the sum of two A objects", MessageName = "Sum")]
        public A Sum(A a1, A a2)
        {
            using (var sr = new StreamReader(HttpContext.Current.Request.InputStream))
            {
                string requestBody = sr.ReadToEnd();
                System.Diagnostics.Debug.WriteLine("Request Body: " + requestBody);
            }

            return new A
            {
                s = a1.s + a2.s,
                k = a1.k + a2.k,
                f = a1.f + a2.f,
            };
        }

        [WebMethod(
            Description = "Returns the sum of two integers in JSON format",
            MessageName = "AddS"
        )]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int AddS(int x, int y) => x + y;
    }
}
