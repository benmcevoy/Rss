using Rss.Server.Models;
using Rss.Server.Services;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Rss.Server.Controllers.API
{
    public class RefreshController : DbContextApiController
    {
        private readonly IRefreshService _feedService;

        public RefreshController(IRefreshService refreshService, FeedsDbEntities context)
            : base(context)
        {
            _feedService = refreshService;
        }

        public HttpResponseMessage Get()
        {
            var sw = new Stopwatch();

            sw.Start();
            
            _feedService.RefreshAllFeeds();

            Context.SaveChanges();

            sw.Stop();

            var response = new HttpResponseMessage
            {
                Content = new StringContent("<!DOCTYPE html><html><head><title>Refresh</title><meta charset='utf-8'/></head><body><h1>Refresh complete " + sw.ElapsedMilliseconds + "</h1> <br/><a href='/'> Home </a> </body></html>")
            };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
            return response;
        }
    }
}
