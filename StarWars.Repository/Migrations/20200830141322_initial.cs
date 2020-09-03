using Microsoft.EntityFrameworkCore.Migrations;

namespace StarWars.Repository.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Episodes",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Episodes", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "CharacterFriendships",
                columns: table => new
                {
                    CharacterName = table.Column<string>(nullable: false),
                    FriendName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterFriendships", x => new { x.CharacterName, x.FriendName });
                    table.ForeignKey(
                        name: "FK_CharacterFriendships_Characters_CharacterName",
                        column: x => x.CharacterName,
                        principalTable: "Characters",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterFriendships_Characters_FriendName",
                        column: x => x.FriendName,
                        principalTable: "Characters",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CharacterEpisodes",
                columns: table => new
                {
                    CharacterName = table.Column<string>(nullable: false),
                    EpisodeName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterEpisodes", x => new { x.CharacterName, x.EpisodeName });
                    table.ForeignKey(
                        name: "FK_CharacterEpisodes_Characters_CharacterName",
                        column: x => x.CharacterName,
                        principalTable: "Characters",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterEpisodes_Episodes_EpisodeName",
                        column: x => x.EpisodeName,
                        principalTable: "Episodes",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterEpisodes_EpisodeName",
                table: "CharacterEpisodes",
                column: "EpisodeName");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterFriendships_FriendName",
                table: "CharacterFriendships",
                column: "FriendName");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_Name",
                table: "Characters",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Episodes_Name",
                table: "Episodes",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterEpisodes");

            migrationBuilder.DropTable(
                name: "CharacterFriendships");

            migrationBuilder.DropTable(
                name: "Episodes");

            migrationBuilder.DropTable(
                name: "Characters");
        }
    }
}
