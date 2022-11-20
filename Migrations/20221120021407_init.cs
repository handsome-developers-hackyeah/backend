using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Comptee.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "_Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    HaveAvatar = table.Column<bool>(type: "boolean", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    Role = table.Column<string>(type: "text", nullable: true),
                    City = table.Column<string>(type: "text", nullable: true),
                    Region = table.Column<string>(type: "text", nullable: true),
                    Rank = table.Column<int>(type: "integer", nullable: true),
                    PlotSize = table.Column<int>(type: "integer", nullable: true),
                    NumberOfResidents = table.Column<int>(type: "integer", nullable: true),
                    BanedPost = table.Column<int>(type: "integer", nullable: true),
                    IsBan = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "_Post",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Localization = table.Column<string>(type: "text", nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: false),
                    ReportCount = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Post", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Post__Users_UserId",
                        column: x => x.UserId,
                        principalTable: "_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "_Comment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    PostId = table.Column<Guid>(type: "uuid", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Comment", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Comment__Post_PostId",
                        column: x => x.PostId,
                        principalTable: "_Post",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Comment__Users_UserId",
                        column: x => x.UserId,
                        principalTable: "_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "_ReportedPost",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PostId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReporterId = table.Column<Guid>(type: "uuid", nullable: false),
                    ByReport = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ReportedPost", x => x.Id);
                    table.ForeignKey(
                        name: "FK__ReportedPost__Post_PostId",
                        column: x => x.PostId,
                        principalTable: "_Post",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__ReportedPost__Users_ReporterId",
                        column: x => x.ReporterId,
                        principalTable: "_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "_Responds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    PostId = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Responds", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Responds__Post_PostId",
                        column: x => x.PostId,
                        principalTable: "_Post",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Responds__Users_UserId",
                        column: x => x.UserId,
                        principalTable: "_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX__Comment_PostId",
                table: "_Comment",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX__Comment_UserId",
                table: "_Comment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX__Post_UserId",
                table: "_Post",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX__ReportedPost_PostId",
                table: "_ReportedPost",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX__ReportedPost_ReporterId",
                table: "_ReportedPost",
                column: "ReporterId");

            migrationBuilder.CreateIndex(
                name: "IX__Responds_PostId",
                table: "_Responds",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX__Responds_UserId",
                table: "_Responds",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "_Comment");

            migrationBuilder.DropTable(
                name: "_ReportedPost");

            migrationBuilder.DropTable(
                name: "_Responds");

            migrationBuilder.DropTable(
                name: "_Post");

            migrationBuilder.DropTable(
                name: "_Users");
        }
    }
}
