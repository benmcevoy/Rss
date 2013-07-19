using Rss.Server.PostModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rss.Server.Services;

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
        public ActionResult AddFeed(AddFeedDto addFeedPostModel)
        {
            var folder = _folderService.Get(addFeedPostModel.Folder);

            if (folder == null && ! string.IsNullOrEmpty(addFeedPostModel.Folder))
            {
                folder = _folderService.Create(addFeedPostModel.Folder);
            }

            var feedId = _feedService.Add(addFeedPostModel.Url, folder.Id);

            _feedService.Refresh(feedId);

            return View();
        }

    }
}
