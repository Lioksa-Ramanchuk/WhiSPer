using System.Web.Http;
using System.Web.Mvc;
using Lab2.Models;

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
