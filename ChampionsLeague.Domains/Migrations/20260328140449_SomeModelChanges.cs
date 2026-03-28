using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChampionsLeague.Domains.Migrations
{
    /// <inheritdoc />
    public partial class SomeModelChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Zitplaats",
                table: "Zitplaats");

            migrationBuilder.RenameTable(
                name: "Zitplaats",
                newName: "Zitplaatsen");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Zitplaatsen",
                table: "Zitplaatsen",
                column: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Zitplaatsen",
                table: "Zitplaatsen");

            migrationBuilder.RenameTable(
                name: "Zitplaatsen",
                newName: "Zitplaats");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Zitplaats",
                table: "Zitplaats",
                column: "id");
        }
    }
}
