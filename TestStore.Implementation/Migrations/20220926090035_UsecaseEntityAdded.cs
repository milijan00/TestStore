using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestStore.Implementation.Migrations
{
    public partial class UsecaseEntityAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usecases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usecases", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleUsecases",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    UsecaseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleUsecases", x => new { x.RoleId, x.UsecaseId });
                    table.ForeignKey(
                        name: "FK_RoleUsecases_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleUsecases_Usecases_UsecaseId",
                        column: x => x.UsecaseId,
                        principalTable: "Usecases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoleUsecases_UsecaseId",
                table: "RoleUsecases",
                column: "UsecaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Usecases_Name",
                table: "Usecases",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleUsecases");

            migrationBuilder.DropTable(
                name: "Usecases");
        }
    }
}
