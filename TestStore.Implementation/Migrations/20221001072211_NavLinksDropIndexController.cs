using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestStore.Implementation.Migrations
{
    public partial class NavLinksDropIndexController : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_NavLinks_Controller",
                table: "NavLinks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_NavLinks_Controller",
                table: "NavLinks",
                column: "Controller",
                unique: true);
        }
    }
}
