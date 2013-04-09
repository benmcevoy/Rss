using Rss.Server.Filters;
using Rss.Server.Models;
using Rss.Server.Services;
using System;
using System.Web.Http;

namespace Rss.Server.Controllers.API
{
    public class FolderController : ApiController
    {
        private readonly IFolderService _folderService;
        private readonly IFeedService _feedService;

        public FolderController(IFolderService folderService, IFeedService feedService)
        {
            _folderService = folderService;
            _feedService = feedService;
        }

        /// <summary>
        /// get the root folder
        /// </summary>
        /// <returns></returns>
        [ApiCache(60)]
        public RootFolder Get()
        {
            return _folderService.GetRoot();
        }

        /// <summary>
        /// get folder by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ApiCache(60)]
        public Folder Get(Guid id)
        {
            return _folderService.Get(id);
        }

        /// <summary>
        /// delete this folder and unsubscribe all feeds in it
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public void Unsubscribe(Guid id)
        {
            var folder = _folderService.Get(id);

            foreach (var feed in folder.Feeds)
            {
                _feedService.Unsubscribe(feed.Id);
            }

            _folderService.Delete(folder.Id);
        }

        /// <summary>
        /// rename the folder
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public void Rename(Guid id, string name)
        {
            _feedService.Rename(id, name);
        }

        /// <summary>
        /// mark all feeds and items in this folder as read
        /// </summary>
        /// <param name="id"></param>
        public Folder Mark(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
