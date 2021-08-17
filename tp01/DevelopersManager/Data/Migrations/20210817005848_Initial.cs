using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeveloperModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GraduationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmployedStatus = table.Column<bool>(type: "bit", nullable: false),
                    PublishedApps = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeveloperModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MobileAppModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PublishedStatus = table.Column<bool>(type: "bit", nullable: false),
                    PublishedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DevId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeveloperId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MobileAppModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MobileAppModel_DeveloperModel_DeveloperId",
                        column: x => x.DeveloperId,
                        principalTable: "DeveloperModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MobileAppModel_DeveloperId",
                table: "MobileAppModel",
                column: "DeveloperId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MobileAppModel");

            migrationBuilder.DropTable(
                name: "DeveloperModel");
        }
    }
}
