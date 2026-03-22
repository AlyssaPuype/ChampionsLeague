using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChampionsLeague.Domains.Migrations
{
    /// <inheritdoc />
    public partial class AddStadionvakCodeAndZitplaatsNummer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(    
               name: "nummer",
               table: "Zitplaats",
               newName: "ZitplaatsNummer");

            migrationBuilder.AlterColumn<string>(
                name: "ZitplaatsNummer",
                table: "Zitplaats",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Stadionvak",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Stadionvak");

            migrationBuilder.AlterColumn<int>(
                name: "nummer",
                table: "Zitplaats",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.RenameColumn(    
                name: "ZitplaatsNummer",
                table: "Zitplaats",
                newName: "nummer");
        }
    }
}
