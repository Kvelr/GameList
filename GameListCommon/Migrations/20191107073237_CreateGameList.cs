using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameListCommon.Migrations
{
    public partial class CreateGameList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(nullable: false),
                    ContestName = table.Column<string>(nullable: true),
                    HomeTeamName = table.Column<string>(nullable: true),
                    AwayTeamName = table.Column<string>(nullable: true),
                    SportType = table.Column<int>(nullable: false),
                    LeagueName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Games");
        }
    }
}
