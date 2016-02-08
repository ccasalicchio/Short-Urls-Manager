using Shorten_Urls.Models;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Shorten_Urls
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static string Website_Name;
        public static string Website_Url;
        public static int COUNT_USERS;
        public static int COUNT_URLS;
        public static XmlIO IO;
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Website_Name = Helpers.WEBSITE_NAME;
            Website_Url = Helpers.WEBSITE_URL;
            IO = new XmlIO();
        }
        protected void Application_BeginRequest()
        {
            COUNT_USERS = new UserRepository().Users.Count;
            COUNT_URLS = new UrlRepository().Urls.Count;
            IO.Reload();
        }

        void Application_Error(object sender, EventArgs e)
        {

            Exception exc = Server.GetLastError();

            if (exc.GetType() == typeof(HttpException))
            {
                if (exc.Message.Contains("NoCatch") || exc.Message.Contains("maxUrlLength"))
                    return;
                Server.Transfer("~/500.html");
            }
            string error = "<h2>Error in Website</h2>\n";
            error += "<p>Error: " + exc.Message + "</p>\n";
            error += "<p>Stack:<br/>"+exc.StackTrace;

            Email Email = new Email();
            Email.SendMail(EmailType.Error, true, error, Email.SITE_ADMIN, "Webmaster");

            Server.ClearError();
            Server.Transfer("~/500.html");
        }
    }
}
