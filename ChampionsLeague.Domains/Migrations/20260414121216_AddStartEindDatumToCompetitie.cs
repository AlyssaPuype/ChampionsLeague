using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChampionsLeague.Domains.Migrations
{
    /// <inheritdoc />
    public partial class AddStartEindDatumToCompetitie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "EindDatum",
                table: "Competitie",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "StartDatum",
                table: "Competitie",
                type: "date",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EindDatum",
                table: "Competitie");

            migrationBuilder.DropColumn(
                name: "StartDatum",
                table: "Competitie");
        }
    }
}
