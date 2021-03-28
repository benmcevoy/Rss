using System.Data.SqlServerCe;
using Rss.Server.PostModel;
using System.Web.Mvc;
using Rss.Server.Services;
using System.Configuration;
using System.Threading.Tasks;

namespace Rss.Server.Controllers
{
    public class SystemController : Controller
    {
        private readonly IFeedService _feedService;
        private readonly IFolderService _folderService;
        
        public SystemController(IFeedService feedService, IFolderService folderService)
        {
            _feedService = feedService;
            _folderService = folderService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddFeed()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddFeed(AddFeedDto addFeedPostModel)
        {
            var folder = _folderService.Get(addFeedPostModel.Folder);

            if (folder == null && !string.IsNullOrEmpty(addFeedPostModel.Folder))
            {
                folder = _folderService.Create(addFeedPostModel.Folder);
            }

            var feedId = _feedService.Add(addFeedPostModel.Url, folder.Id);

            _feedService.Save();

            await _feedService.Refresh(feedId);

            return View();
        }
        
        [HttpPost]
        public ActionResult Repair()
        {
            var engine = new SqlCeEngine(ConfigurationManager.ConnectionStrings["FeedsDbEntities"].ConnectionString);

            // Specify null destination connection string for in-place repair
            engine.Repair(null, RepairOption.RecoverAllOrFail);

            return View();
        }

    }
}
