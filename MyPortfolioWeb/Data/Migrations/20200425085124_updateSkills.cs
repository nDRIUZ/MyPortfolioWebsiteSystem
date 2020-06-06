using Microsoft.EntityFrameworkCore.Migrations;

namespace MyPortfolioWeb.Data.Migrations
{
    public partial class updateSkills : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SkillPercentage",
                table: "Skills");

            migrationBuilder.AddColumn<string>(
                name: "SkillImg",
                table: "Skills",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SkillImg",
                table: "Skills");

            migrationBuilder.AddColumn<int>(
                name: "SkillPercentage",
                table: "Skills",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
