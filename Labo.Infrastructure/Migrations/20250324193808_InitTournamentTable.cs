using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Labo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitTournamentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tournaments");
        }
    }
}
