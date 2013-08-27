using System.Web.Mvc;
using Rss.Server.Filters;

namespace Rss.Server
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new SessionAuthorizationFilter());
        }
    }
}