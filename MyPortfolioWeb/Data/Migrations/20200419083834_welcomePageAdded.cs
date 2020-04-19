using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyPortfolioWeb.Data.Migrations
{
    public partial class welcomePageAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WelcomePages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Img = table.Column<byte[]>(nullable: true),
                    WelcomeWorkingTitle = table.Column<string>(nullable: true),
                    WelcomeText = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WelcomePages", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WelcomePages");
        }
    }
}
