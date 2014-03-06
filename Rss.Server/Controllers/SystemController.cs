using System.Data.SqlServerCe;
using Rss.Server.Models;
using Rss.Server.PostModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rss.Server.Services;
using System.Configuration;

namespace Rss.Server.Controllers
{
    public class SystemController : Controller
    {
        private readonly IFeedService _feedService;
        private readonly IFolderService _folderService;
        private readonly IItemService _itemService;
        private readonly FeedsDbEntities _context;

        public SystemController(IFeedService feedService, IFolderService folderService, IItemService itemService, FeedsDbEntities context)
        {
            _feedService = feedService;
            _folderService = folderService;
            _itemService = itemService;
            _context = context;
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
        public ActionResult AddFeed(AddFeedDto addFeedPostModel)
        {
            var folder = _folderService.Get(addFeedPostModel.Folder);

            if (folder == null && !string.IsNullOrEmpty(addFeedPostModel.Folder))
            {
                folder = _folderService.Create(addFeedPostModel.Folder);
            }

            var feedId = _feedService.Add(addFeedPostModel.Url, folder.Id);

            _feedService.Refresh(feedId);

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
