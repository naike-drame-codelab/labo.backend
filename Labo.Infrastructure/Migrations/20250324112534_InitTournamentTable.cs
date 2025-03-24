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
            migrationBuilder.AddCheckConstraint(
                name: "CK_Tournament_MaxPlayers",
                table: "Tournaments",
                sql: "MaxPlayers BETWEEN 2 AND 32");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Tournament_MinPlayers",
                table: "Tournaments",
                sql: "MinPlayers BETWEEN 2 AND 32");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Tournament_MaxPlayers",
                table: "Tournaments");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Tournament_MinPlayers",
                table: "Tournaments");
        }
    }
}
