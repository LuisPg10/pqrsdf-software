using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Traceability : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Area",
                table: "Solicitudes");

            migrationBuilder.AddColumn<Guid>(
                name: "AreaId",
                table: "Solicitudes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "Solicitudes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "FiledCounters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    LastNumber = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiledCounters", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Solicitudes_AreaId",
                table: "Solicitudes",
                column: "AreaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Solicitudes_Areas_AreaId",
                table: "Solicitudes",
                column: "AreaId",
                principalTable: "Areas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Solicitudes_Areas_AreaId",
                table: "Solicitudes");

            migrationBuilder.DropTable(
                name: "FiledCounters");

            migrationBuilder.DropIndex(
                name: "IX_Solicitudes_AreaId",
                table: "Solicitudes");

            migrationBuilder.DropColumn(
                name: "AreaId",
                table: "Solicitudes");

            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "Solicitudes");

            migrationBuilder.AddColumn<string>(
                name: "Area",
                table: "Solicitudes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
