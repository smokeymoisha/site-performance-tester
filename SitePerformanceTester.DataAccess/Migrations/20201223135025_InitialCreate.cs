using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SitePerformanceTester.DataAccess.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SitemapRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SitemapUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SitemapRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SitemapUrls",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponseTime = table.Column<long>(type: "bigint", nullable: false),
                    SitemapRequestId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SitemapUrls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SitemapUrls_SitemapRequests_SitemapRequestId",
                        column: x => x.SitemapRequestId,
                        principalTable: "SitemapRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SitemapUrls_SitemapRequestId",
                table: "SitemapUrls",
                column: "SitemapRequestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SitemapUrls");

            migrationBuilder.DropTable(
                name: "SitemapRequests");
        }
    }
}
