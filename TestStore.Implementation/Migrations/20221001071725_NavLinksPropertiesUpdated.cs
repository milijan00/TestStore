using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestStore.Implementation.Migrations
{
    public partial class NavLinksPropertiesUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_NavLinks_Action",
                table: "NavLinks");

            migrationBuilder.CreateIndex(
                name: "IX_NavLinks_Controller",
                table: "NavLinks",
                column: "Controller",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_NavLinks_Controller",
                table: "NavLinks");

            migrationBuilder.CreateIndex(
                name: "IX_NavLinks_Action",
                table: "NavLinks",
                column: "Action",
                unique: true);
        }
    }
}
