using Rss.Server.Services;
using StructureMap;
using System.Web;
using System.Web.Mvc;

namespace Rss.Server.Filters
{
    public class SessionAuthorizationFilter : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var service = ObjectFactory.GetInstance<CookieService>();

            return service.IsSet(httpContext);
        }
    }
}