using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rss.Api.Data;
using Rss.Api.V1.Model;

namespace Rss.Api.V1
{
    [ApiController, ApiVersion("1")]
    [Route("v{version:apiVersion}/subscription")]
    public class SubscriptionController
    {
        private readonly DatabaseContext _context;
        private readonly RefreshDataService _refreshDataService;

        public SubscriptionController(DatabaseContext context, RefreshDataService refreshDataService)
        {
            _context = context;
            _refreshDataService = refreshDataService;
        }

        [HttpGet]
        public Subscription Get()
        {
            var folders = _context.Folders
                .Include(x => x.Feeds)
                .ThenInclude(x => x.Items);

            return new Subscription
            {
                Folders = folders.Select(Mapper.Map).ToList()
            };
        }

        [HttpPost, Route("Refresh")]
        public async Task Refresh() => await _refreshDataService.Refresh();
    }
}
