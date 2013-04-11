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
        private readonly IItemService _itemService;

        public HomeController(IFeedService feedService, IFolderService folderService, IItemService itemService)
        {
            _feedService = feedService;
            _folderService = folderService;
            _itemService = itemService;
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

        public ActionResult Item(Guid id)
        {
            var model = _itemService.Get(id);
            return View(model);
        }
    }
}
