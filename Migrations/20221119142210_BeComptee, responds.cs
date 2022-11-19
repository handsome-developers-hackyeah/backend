using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Comptee.Migrations
{
    /// <inheritdoc />
    public partial class BeCompteeresponds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BeCompteeActivity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Localization = table.Column<string>(type: "text", nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeCompteeActivity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BeCompteeActivity__Users_UserId",
                        column: x => x.UserId,
                        principalTable: "_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Respond",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    BeCompteeActivityId = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Respond", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Respond_BeCompteeActivity_BeCompteeActivityId",
                        column: x => x.BeCompteeActivityId,
                        principalTable: "BeCompteeActivity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Respond__Users_UserId",
                        column: x => x.UserId,
                        principalTable: "_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BeCompteeActivity_UserId",
                table: "BeCompteeActivity",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Respond_BeCompteeActivityId",
                table: "Respond",
                column: "BeCompteeActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_Respond_UserId",
                table: "Respond",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Respond");

            migrationBuilder.DropTable(
                name: "BeCompteeActivity");
        }
    }
}
