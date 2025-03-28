using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Labo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitMemberTableTournamentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salt = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Elo = table.Column<int>(type: "int", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                    table.CheckConstraint("CK_ELO", "Elo BETWEEN 0 AND 3000");
                });

            migrationBuilder.CreateTable(
                name: "Tournaments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MinPlayers = table.Column<int>(type: "int", nullable: false),
                    MaxPlayers = table.Column<int>(type: "int", nullable: false),
                    MinElo = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    MaxElo = table.Column<int>(type: "int", nullable: true, defaultValue: 3000),
                    Categories = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    CurrentRound = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    WomenOnly = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    EndOfRegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tournaments", x => x.Id);
                    table.CheckConstraint("CK_Tournament_EndOfRegistrationDate", "EndOfRegistrationDate > DATEADD(day, MinPlayers, GETDATE())");
                    table.CheckConstraint("CK_Tournament_MaxElo", "MaxElo >= 0 AND MaxElo <= 3000");
                    table.CheckConstraint("CK_Tournament_MaxPlayers", "MaxPlayers BETWEEN 2 AND 32");
                    table.CheckConstraint("CK_Tournament_MinElo", "MinElo >= 0 AND MinElo <= 3000");
                    table.CheckConstraint("CK_Tournament_MinElo_MaxElo", "MinElo <= MaxElo");
                    table.CheckConstraint("CK_Tournament_MinPlayers", "MinPlayers BETWEEN 2 AND 32");
                    table.CheckConstraint("CK_Tournament_MinPlayers_MaxPlayers", "MinPlayers <= MaxPlayers");
                });

            migrationBuilder.CreateTable(
                name: "Inscriptions",
                columns: table => new
                {
                    PlayersId = table.Column<int>(type: "int", nullable: false),
                    TournamentsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inscriptions", x => new { x.PlayersId, x.TournamentsId });
                    table.ForeignKey(
                        name: "FK_Inscriptions_Members_PlayersId",
                        column: x => x.PlayersId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Inscriptions_Tournaments_TournamentsId",
                        column: x => x.TournamentsId,
                        principalTable: "Tournaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "Id", "BirthDate", "Elo", "Email", "Gender", "Password", "Role", "Salt", "Username" },
                values: new object[] { 1, new DateTime(1982, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 1500, "lykhun@gmail.com", 1, "��O)�a��=o�bz�\n٠į8����P����\n'�:�Q-n��h��p�Ɋ����>A��", 1, new Guid("00000000-0000-0000-0000-000000000000"), "Checkmate" });

            migrationBuilder.CreateIndex(
                name: "IX_Inscriptions_TournamentsId",
                table: "Inscriptions",
                column: "TournamentsId");

            migrationBuilder.CreateIndex(
                name: "IX_Members_Email",
                table: "Members",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Members_Salt",
                table: "Members",
                column: "Salt",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Members_Username",
                table: "Members",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inscriptions");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "Tournaments");
        }
    }
}
