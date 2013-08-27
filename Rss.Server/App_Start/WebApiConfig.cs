using System.Net.Http.Formatting;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Rss.Server.App_Start;
using Rss.Server.Filters;

namespace Rss.Server
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Filters.Add(new APISessionAuthorizationFilter());

            config.Routes.MapHttpRoute(
                name: "ControllerAndActionAndId",
                routeTemplate: "api/{controller}/{action}/{id}",
                 defaults: new { action = "get", id = RouteParameter.Optional }
            );

            SetContentNegotiator(config);
        }

        private static void SetContentNegotiator(HttpConfiguration config)
        {
            var jsonFormatter = new JsonMediaTypeFormatter
                {
                    SerializerSettings =
                        {
                            Formatting = Formatting.Indented,
                            ContractResolver = new CamelCasePropertyNamesContractResolver()
                        }
                };

            config.Services.Replace(typeof(IContentNegotiator), new JsonContentNegotiator(jsonFormatter));
        }
    }
}
