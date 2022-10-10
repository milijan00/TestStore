using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestStore.Implementation.Migrations
{
    public partial class NavLinksAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NavLinks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Route = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NavLinks", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NavLinks_Name",
                table: "NavLinks",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NavLinks_Route",
                table: "NavLinks",
                column: "Route",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NavLinks");
        }
    }
}
