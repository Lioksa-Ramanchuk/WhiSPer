using Lab2.Models;
using System.Web.Http;
using System.Web.Mvc;

namespace Lab2
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        public static readonly string AppRamStorageName = "RamModel";

        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            Application[AppRamStorageName] = new RamModel();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        }
    }
}
