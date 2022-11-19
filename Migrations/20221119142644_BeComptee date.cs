using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Comptee.Migrations
{
    /// <inheritdoc />
    public partial class BeCompteedate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "BeCompteeActivity",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Rank",
                table: "_Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "BeCompteeActivity");

            migrationBuilder.DropColumn(
                name: "Rank",
                table: "_Users");
        }
    }
}
