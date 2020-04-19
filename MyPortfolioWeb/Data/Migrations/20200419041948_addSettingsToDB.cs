using Microsoft.EntityFrameworkCore.Migrations;

namespace MyPortfolioWeb.Data.Migrations
{
    public partial class addSettingsToDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacebookLink = table.Column<string>(nullable: true),
                    InstaLink = table.Column<string>(nullable: true),
                    LinkedInLink = table.Column<string>(nullable: true),
                    TwitterLink = table.Column<string>(nullable: true),
                    FullName = table.Column<string>(nullable: true),
                    ResumeLink = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Settings");
        }
    }
}
