using System.Data.Entity;
using Rss.Server.Models;
using Rss.Server.Services;
using Rss.Server.ViewModels;
using System;
using System.Web.Mvc;

namespace Rss.Server.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFeedService _feedService;
        private readonly IFolderService _folderService;
        private readonly IItemService _itemService;
        private readonly FeedsDbEntities _context;

        public HomeController(IFeedService feedService, IFolderService folderService, IItemService itemService, FeedsDbEntities context)
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

        public ActionResult Folder(Guid id)
        {
            var items = _folderService.GetItems(id, ReadOptions.Unread);

            var vm = new FolderItemsViewModel
                {
                    Items = items,
                    FolderId = id,
                    FolderName = _folderService.Get(id).Name
                };

            return PartialView(vm);
        }

        public ActionResult Feed(Guid id)
        {
            var model = _feedService.Get(id, ReadOptions.Unread);
            return PartialView(model);
        }

        public ActionResult Item(Guid id)
        {
            var model = _itemService.Get(id);
            _itemService.Read(id);
            _context.SaveChanges();
            return PartialView(model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
