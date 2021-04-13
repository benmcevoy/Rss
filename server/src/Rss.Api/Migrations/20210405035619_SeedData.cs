using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Rss.Api.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var devFolderId = Guid.NewGuid();
            var epoch = new DateTime(2000, 1, 1);

            migrationBuilder.InsertData("Folders", new[] { "Id", "Name", "LastUpdateDateTime" }, new object[] { devFolderId, "Dev", epoch });
            migrationBuilder.InsertData("Feeds",
                new[] { "Id","Name","FolderId","LastUpdateDateTime","FeedUrl","HtmlUrl","UpdatePeriod","UpdateFrequency","FavIcon" },
                new object[] { Guid.NewGuid(), "Reflective Perspective", devFolderId, epoch, "http://feeds.feedburner.com/ReflectivePerspective", "http://blog.cwa.me.uk/", "Daily", 1, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
