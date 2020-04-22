using Microsoft.EntityFrameworkCore.Migrations;

namespace MyPortfolioWeb.Data.Migrations
{
    public partial class AddPortfolio1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Portfolios");

            migrationBuilder.AddColumn<string>(
                name: "Img",
                table: "Portfolios",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Img",
                table: "Portfolios");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Portfolios",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
