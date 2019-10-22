using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Arclight.Database.Auth.Migrations
{
    public partial class HelloWorld : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "account",
                columns: table => new
                {
                    Id = table.Column<uint>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(maxLength: 32, nullable: false),
                    Password = table.Column<string>(maxLength: 128, nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    SessionKey = table.Column<ulong>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_account", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "server",
                columns: table => new
                {
                    Id = table.Column<uint>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 32, nullable: false),
                    Host = table.Column<string>(maxLength: 64, nullable: false),
                    Port = table.Column<ushort>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_server", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "account_character_count",
                columns: table => new
                {
                    Id = table.Column<uint>(nullable: false),
                    ServerId = table.Column<uint>(nullable: false),
                    Count = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_account_character_count", x => x.Id);
                    table.ForeignKey(
                        name: "FK_account_character_count_account_Id",
                        column: x => x.Id,
                        principalTable: "account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_account_character_count_server_ServerId",
                        column: x => x.ServerId,
                        principalTable: "server",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "server_cluster",
                columns: table => new
                {
                    Id = table.Column<uint>(nullable: false),
                    Index = table.Column<uint>(nullable: false),
                    Host = table.Column<string>(maxLength: 64, nullable: false),
                    Port = table.Column<ushort>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_server_cluster", x => new { x.Id, x.Index });
                    table.ForeignKey(
                        name: "FK_server_cluster_server_Id",
                        column: x => x.Id,
                        principalTable: "server",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "server",
                columns: new[] { "Id", "Host", "Name", "Port" },
                values: new object[] { 1u, "127.0.0.1", "Arclight Server", (ushort)10100 });

            migrationBuilder.InsertData(
                table: "server_cluster",
                columns: new[] { "Id", "Index", "Host", "Port" },
                values: new object[] { 1u, 1u, "127.0.0.1", (ushort)10200 });

            migrationBuilder.CreateIndex(
                name: "IX_account_Username",
                table: "account",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_account_Id_SessionKey",
                table: "account",
                columns: new[] { "Id", "SessionKey" });

            migrationBuilder.CreateIndex(
                name: "IX_account_character_count_ServerId",
                table: "account_character_count",
                column: "ServerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "account_character_count");

            migrationBuilder.DropTable(
                name: "server_cluster");

            migrationBuilder.DropTable(
                name: "account");

            migrationBuilder.DropTable(
                name: "server");
        }
    }
}
