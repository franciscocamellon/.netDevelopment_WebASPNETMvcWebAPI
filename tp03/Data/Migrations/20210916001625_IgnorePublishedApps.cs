using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class IgnorePublishedApps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublishedApps",
                table: "Developers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PublishedApps",
                table: "Developers",
                type: "int",
                nullable: true);
        }
    }
}
