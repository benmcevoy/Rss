using System.Web.Http;
using Rss.Server.Models;

namespace Rss.Server.Controllers.API
{
    public abstract class DbContextApiController : ApiController
    {
        protected readonly FeedsDbEntities Context;

        protected DbContextApiController(FeedsDbEntities context)
        {
            Context = context;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Context.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}