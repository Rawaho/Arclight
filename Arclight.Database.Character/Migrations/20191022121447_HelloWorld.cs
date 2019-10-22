using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Arclight.Database.Character.Migrations
{
    public partial class HelloWorld : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "character",
                columns: table => new
                {
                    Id = table.Column<uint>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AccountId = table.Column<uint>(nullable: false),
                    Index = table.Column<byte>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Class = table.Column<byte>(nullable: false),
                    Level = table.Column<byte>(nullable: false),
                    Appearance = table.Column<ulong>(nullable: false),
                    MapId = table.Column<ushort>(nullable: false),
                    X = table.Column<float>(nullable: false),
                    Y = table.Column<float>(nullable: false),
                    Z = table.Column<float>(nullable: false),
                    O = table.Column<float>(nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_character", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_character_Name",
                table: "character",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_character_AccountId_Index",
                table: "character",
                columns: new[] { "AccountId", "Index" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "character");
        }
    }
}
