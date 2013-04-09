using Rss.Server.Models;
using Rss.Server.Services;
using System;
using System.Web.Mvc;

namespace Rss.Server.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFeedService _feedService;
        private readonly IFolderService _folderService;

        public HomeController(IFeedService feedService, IFolderService folderService)
        {
            _feedService = feedService;
            _folderService = folderService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Folder(Guid id)
        {
            var model = _folderService.Get(id);
            return View(model);
        }

        public ActionResult Feed(Guid id)
        {
            var model = _feedService.Get(id, ReadOptions.Unread);
            return View(model);
        }
    }
}
