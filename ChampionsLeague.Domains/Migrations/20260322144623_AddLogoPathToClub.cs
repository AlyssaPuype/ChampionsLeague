using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChampionsLeague.Domains.Migrations
{
    /// <inheritdoc />
    public partial class AddLogoPathToClub : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LogoPath",
                table: "Club",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogoPath",
                table: "Club");
        }
    }
}
