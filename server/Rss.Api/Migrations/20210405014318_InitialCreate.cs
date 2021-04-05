﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Rss.Api.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Folders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    LastUpdateDateTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Folders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Feeds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    FolderId = table.Column<Guid>(type: "TEXT", nullable: false),
                    LastUpdateDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FeedUrl = table.Column<string>(type: "TEXT", nullable: true),
                    HtmlUrl = table.Column<string>(type: "TEXT", nullable: true),
                    LastBuildDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatePeriod = table.Column<string>(type: "TEXT", nullable: true),
                    UpdateFrequency = table.Column<int>(type: "INTEGER", nullable: false),
                    FavIcon = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feeds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feeds_Folders_FolderId",
                        column: x => x.FolderId,
                        principalTable: "Folders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Raw = table.Column<string>(type: "TEXT", nullable: true),
                    Content = table.Column<string>(type: "TEXT", nullable: true),
                    Snippet = table.Column<string>(type: "TEXT", nullable: true),
                    ReadDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PublishedDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LinkId = table.Column<string>(type: "TEXT", nullable: true),
                    FeedId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_Feeds_FeedId",
                        column: x => x.FeedId,
                        principalTable: "Feeds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Feeds_FolderId",
                table: "Feeds",
                column: "FolderId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_FeedId",
                table: "Items",
                column: "FeedId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Feeds");

            migrationBuilder.DropTable(
                name: "Folders");
        }
    }
}
