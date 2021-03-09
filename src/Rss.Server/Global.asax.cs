using System.Diagnostics;
using System.Security.Principal;
using System.Web.Security;
using System.Web.SessionState;
using Clutch.Diagnostics.EntityFramework;
using Rss.Server.App_Start;
using StackExchange.Profiling;
using System;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Rss.Server
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //AreaRegistration.RegisterAllAreas();

            StructuremapMvc.Start();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AddDbTracing();
        }

        protected void Application_PostAuthorizeRequest()
        {
            if (IsWebApiRequest())
            {
                HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
            }
        }

        private bool IsWebApiRequest()
        {
            return HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.StartsWith("~/api");
        }

        [Conditional("DEBUG")]
        private static void AddDbTracing()
        {
            DbTracing.Enable(
                new GenericDbTracingListener()
                    .OnFinished(
                        c =>
                        Debug.WriteLine("-- Command finished - time: {0}{1}{2}", c.Duration, Environment.NewLine,
                                        c.Command.ToTraceString()))
                    .OnFailed(
                        c =>
                        Debug.WriteLine("-- Command failed - time: {0}{1}{2}", c.Duration, Environment.NewLine,
                                        c.Command.ToTraceString()))
                );
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var ctx = HttpContext.Current;
            var sb = new StringBuilder();
            sb.Append(ctx.Request.Url + Environment.NewLine);
            sb.Append("Source:" + Environment.NewLine + ctx.Server.GetLastError().Source);
            sb.Append("Message:" + Environment.NewLine + ctx.Server.GetLastError().Message);
            sb.Append("Stack Trace:" + Environment.NewLine + ctx.Server.GetLastError().StackTrace);

            ctx.Response.Write(sb.ToString());
        }
    }
}