﻿using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using Rss.Server.Models;
using Rss.Server.PostModel;
using Rss.Server.Services;
using Rss.Server.ViewModels;

namespace Rss.Server.Controllers.API.v2
{
    [EnableCors(origins: "http://rss.local", headers:"*", methods:"*")]
    public class V2_FolderController : DbContextApiController
    {
        private readonly IFolderService _folderService;
        private readonly IFeedService _feedService;

        public V2_FolderController(IFolderService folderService, IFeedService feedService, FeedsDbEntities context)
            : base(context)
        {
            _folderService = folderService;
            _feedService = feedService;
        }

        /// <summary>
        /// get the root folder
        /// </summary>
        /// <returns></returns>
        public RootViewModel Get()
        {
            try
            {
                var root = _folderService.GetRoot();

                return new RootViewModel
                    {
                        Feeds = root.Feeds
                                .OrderBy(f => f.Name)
                                .Select(feed => new FeedViewModel
                            {
                                Id = feed.Id.ToString("D"),
                                Name = feed.Name,
                                ItemCount = feed.ItemCount,
                                UnreadClass = feed.ItemCount > 0 ? "unread" : "read"
                            }),
                        Folders = root.Folders
                                    .OrderBy(f => f.Name)
                                    .Select(folder => new FolderViewModel
                            {
                                Id = folder.Id.ToString("D"),
                                Name = folder.Name,
                                LastUpdateDateTime = folder.LastUpdateDateTime,
                                Feeds = folder.Feeds
                                        .OrderBy(f => f.Name)
                                        .Select(feed => new FeedViewModel
                                    {
                                        Id = feed.Id.ToString("D"),
                                        Name = feed.Name,
                                        ItemCount = feed.ItemCount,
                                        UnreadClass = feed.ItemCount > 0 ? "unread" : "read"
                                    }).ToList()
                            })
                    };

            }
            catch (Exception ex)
            {
                return new RootViewModel()
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
        // [ApiCache(60)]
        public Folder Get(Guid id)
        {
            return _folderService.Get2(id);
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
            Context.SaveChanges();
        }

        /// <summary>
        /// rename the folder
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public void Rename(RenameDto renameDto)
        {
            _feedService.Rename(renameDto.Id, renameDto.Name);
            Context.SaveChanges();
        }

        /// <summary>
        /// mark all feeds and items in this folder as read
        /// </summary>
        /// <param name="id"></param>
        [HttpPost]
        public void Mark([FromBody]Guid id)
        {
            _folderService.Mark(id, MarkOptions.All);
            Context.SaveChanges();
        }

        [HttpPost]
        public void Refresh([FromBody] Guid id)
        {
            _folderService.Refresh(id);
            Context.SaveChanges();
        }
    }
}
