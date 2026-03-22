using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChampionsLeague.Domains.Migrations
{
    /// <inheritdoc />
    public partial class SyncModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UQ_Zitplaats_VakNummer",
                table: "Zitplaats");

            migrationBuilder.RenameColumn(
                name: "nummer",
                table: "Zitplaats",
                newName: "ZitplaatsNummer");

            migrationBuilder.CreateIndex(
                name: "UQ_Zitplaats_VakNummer",
                table: "Zitplaats",
                columns: new[] { "stadionvak_id", "ZitplaatsNummer" },
                unique: true,
                filter: "[ZitplaatsNummer] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UQ_Zitplaats_VakNummer",
                table: "Zitplaats");

            migrationBuilder.RenameColumn(
                name: "ZitplaatsNummer",
                table: "Zitplaats",
                newName: "nummer");

            migrationBuilder.CreateIndex(
                name: "UQ_Zitplaats_VakNummer",
                table: "Zitplaats",
                columns: new[] { "stadionvak_id", "nummer" },
                unique: true,
                filter: "[nummer] IS NOT NULL");
        }
    }
}
