using Rss.Server.Filters;
using Rss.Server.Models;
using Rss.Server.PostModel;
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
            _folderService =  folderService;
            _feedService =  feedService;
        }

        /// <summary>
        /// get the root folder
        /// </summary>
        /// <returns></returns>
        //[ApiCache(60)]
        public RootFolder Get()
        {
            try
            {

                return _folderService.GetRoot();
            }
            catch(Exception ex)
            {
                return new RootFolder()
                    {
                        Name = ex.ToString()
                    };
            }
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
        [HttpPost]
        public void Unsubscribe([FromBody]Guid id)
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
        /// <returns></returns>
        [HttpPost]
        public void Rename(RenameDto renameDto)
        {
            _feedService.Rename(renameDto.Id, renameDto.Name);
        }

        /// <summary>
        /// mark all feeds and items in this folder as read
        /// </summary>
        /// <param name="id"></param>
        [HttpPost] 
        public Folder Mark([FromBody]Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
