using System.Web.Http;
using Rss.Server.Services;
using StructureMap;

namespace Rss.Server.Filters
{
    public class ApiSessionAuthorizationFilter : AuthorizeAttribute
    {
        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            var service = ObjectFactory.GetInstance<CookieService>();

            return service.IsSet(actionContext);
        }
    }
}