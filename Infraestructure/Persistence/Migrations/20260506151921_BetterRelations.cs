using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class BetterRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SolicitudeTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Time = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudeTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Solicitudes_TypeId",
                table: "Solicitudes",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudeResponses_SolicitudeId",
                table: "SolicitudeResponses",
                column: "SolicitudeId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudeResponses_UserId",
                table: "SolicitudeResponses",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SolicitudeResponses_Solicitudes_SolicitudeId",
                table: "SolicitudeResponses",
                column: "SolicitudeId",
                principalTable: "Solicitudes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SolicitudeResponses_Users_UserId",
                table: "SolicitudeResponses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Solicitudes_SolicitudeTypes_TypeId",
                table: "Solicitudes",
                column: "TypeId",
                principalTable: "SolicitudeTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SolicitudeResponses_Solicitudes_SolicitudeId",
                table: "SolicitudeResponses");

            migrationBuilder.DropForeignKey(
                name: "FK_SolicitudeResponses_Users_UserId",
                table: "SolicitudeResponses");

            migrationBuilder.DropForeignKey(
                name: "FK_Solicitudes_SolicitudeTypes_TypeId",
                table: "Solicitudes");

            migrationBuilder.DropTable(
                name: "SolicitudeTypes");

            migrationBuilder.DropIndex(
                name: "IX_Solicitudes_TypeId",
                table: "Solicitudes");

            migrationBuilder.DropIndex(
                name: "IX_SolicitudeResponses_SolicitudeId",
                table: "SolicitudeResponses");

            migrationBuilder.DropIndex(
                name: "IX_SolicitudeResponses_UserId",
                table: "SolicitudeResponses");
        }
    }
}
