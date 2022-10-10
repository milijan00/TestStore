using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestStore.Implementation.Migrations
{
    public partial class NavLinksUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Route",
                table: "NavLinks",
                newName: "Action");

            migrationBuilder.RenameIndex(
                name: "IX_NavLinks_Route",
                table: "NavLinks",
                newName: "IX_NavLinks_Action");

            migrationBuilder.AddColumn<string>(
                name: "Controller",
                table: "NavLinks",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Controller",
                table: "NavLinks");

            migrationBuilder.RenameColumn(
                name: "Action",
                table: "NavLinks",
                newName: "Route");

            migrationBuilder.RenameIndex(
                name: "IX_NavLinks_Action",
                table: "NavLinks",
                newName: "IX_NavLinks_Route");
        }
    }
}
