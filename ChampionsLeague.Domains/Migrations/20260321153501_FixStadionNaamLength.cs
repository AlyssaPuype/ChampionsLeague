using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChampionsLeague.Domains.Migrations
{
    /// <inheritdoc />
    public partial class FixStadionNaamLength : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationUser",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Competitie",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    naam = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competitie", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Stadion",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    naam = table.Column<string>(type: "varchar(22)", unicode: false, maxLength: 22, nullable: true),
                    capaciteit = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stadion", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    order_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    totale_prijs = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    user_id = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false, defaultValue: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.id);
                    table.ForeignKey(
                        name: "FK_Order_AspNetUser",
                        column: x => x.user_id,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Club",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    stadion_id = table.Column<int>(type: "int", nullable: false),
                    naam = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Club", x => x.id);
                    table.ForeignKey(
                        name: "FK_Club_Stadion",
                        column: x => x.stadion_id,
                        principalTable: "Stadion",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Stadionvak",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    stadion_id = table.Column<int>(type: "int", nullable: false),
                    naam = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    ring = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    type = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    partij = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    capaciteit = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stadionvak", x => x.id);
                    table.ForeignKey(
                        name: "FK_Stadionvak_Stadion",
                        column: x => x.stadion_id,
                        principalTable: "Stadion",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Orderline",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    order_id = table.Column<int>(type: "int", nullable: false),
                    prijs = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orderline", x => x.id);
                    table.ForeignKey(
                        name: "FK_Orderline_Order",
                        column: x => x.order_id,
                        principalTable: "Order",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Match",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    competitie_id = table.Column<int>(type: "int", nullable: false),
                    thuisclub_id = table.Column<int>(type: "int", nullable: false),
                    bezoekersclub_id = table.Column<int>(type: "int", nullable: false),
                    match_date = table.Column<DateOnly>(type: "date", nullable: true),
                    stadion_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Match", x => x.id);
                    table.ForeignKey(
                        name: "FK_Match_Bezoekersclub",
                        column: x => x.bezoekersclub_id,
                        principalTable: "Club",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Match_Competitie",
                        column: x => x.competitie_id,
                        principalTable: "Competitie",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Match_Stadion",
                        column: x => x.stadion_id,
                        principalTable: "Stadion",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Match_Thuisclub",
                        column: x => x.thuisclub_id,
                        principalTable: "Club",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Zitplaats",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    stadionvak_id = table.Column<int>(type: "int", nullable: false),
                    ZitplaatsNummer = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zitplaats", x => x.id);
                    table.ForeignKey(
                        name: "FK_Zitplaats_Stadionvak",
                        column: x => x.stadionvak_id,
                        principalTable: "Stadionvak",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Abonnement",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    club_id = table.Column<int>(type: "int", nullable: false),
                    zitplaats_id = table.Column<int>(type: "int", nullable: false),
                    orderlineid = table.Column<int>(type: "int", nullable: true),
                    prijs = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abonnement", x => x.id);
                    table.ForeignKey(
                        name: "FK_Abonnement_Club",
                        column: x => x.club_id,
                        principalTable: "Club",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Abonnement_Orderline",
                        column: x => x.orderlineid,
                        principalTable: "Orderline",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Abonnement_Zitplaats",
                        column: x => x.zitplaats_id,
                        principalTable: "Zitplaats",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Ticket",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    match_id = table.Column<int>(type: "int", nullable: false),
                    zitplaats_id = table.Column<int>(type: "int", nullable: false),
                    orderlineid = table.Column<int>(type: "int", nullable: true),
                    prijs = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    status = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket", x => x.id);
                    table.ForeignKey(
                        name: "FK_Ticket_Match",
                        column: x => x.match_id,
                        principalTable: "Match",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Ticket_Orderline",
                        column: x => x.orderlineid,
                        principalTable: "Orderline",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Ticket_Zitplaats",
                        column: x => x.zitplaats_id,
                        principalTable: "Zitplaats",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Voucher",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ticket_id = table.Column<int>(type: "int", nullable: false),
                    code = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voucher", x => x.id);
                    table.ForeignKey(
                        name: "FK_Voucher_Ticket",
                        column: x => x.ticket_id,
                        principalTable: "Ticket",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Abonnement_club_id",
                table: "Abonnement",
                column: "club_id");

            migrationBuilder.CreateIndex(
                name: "IX_Abonnement_orderlineid",
                table: "Abonnement",
                column: "orderlineid");

            migrationBuilder.CreateIndex(
                name: "UQ__Abonneme__4ECF5B70B7B226DD",
                table: "Abonnement",
                column: "zitplaats_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Club__3EC28F0C79C20726",
                table: "Club",
                column: "stadion_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Club__72E1CD788D8247FA",
                table: "Club",
                column: "naam",
                unique: true,
                filter: "[naam] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ__Competit__72E1CD78E8C69DBC",
                table: "Competitie",
                column: "naam",
                unique: true,
                filter: "[naam] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Match_bezoekersclub_id",
                table: "Match",
                column: "bezoekersclub_id");

            migrationBuilder.CreateIndex(
                name: "IX_Match_competitie_id",
                table: "Match",
                column: "competitie_id");

            migrationBuilder.CreateIndex(
                name: "IX_Match_stadion_id",
                table: "Match",
                column: "stadion_id");

            migrationBuilder.CreateIndex(
                name: "UQ_Match",
                table: "Match",
                columns: new[] { "thuisclub_id", "bezoekersclub_id", "match_date" },
                unique: true,
                filter: "[match_date] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Order_user_id",
                table: "Order",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Orderline_order_id",
                table: "Orderline",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "UQ__Stadion__72E1CD78DAD7BB7D",
                table: "Stadion",
                column: "naam",
                unique: true,
                filter: "[naam] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ_Stadionvak_Combinatie",
                table: "Stadionvak",
                columns: new[] { "stadion_id", "ring", "type", "partij" },
                unique: true,
                filter: "[ring] IS NOT NULL AND [type] IS NOT NULL AND [partij] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_orderlineid",
                table: "Ticket",
                column: "orderlineid");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_zitplaats_id",
                table: "Ticket",
                column: "zitplaats_id");

            migrationBuilder.CreateIndex(
                name: "UQ_Ticket_SeatMatch",
                table: "Ticket",
                columns: new[] { "match_id", "zitplaats_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Voucher__357D4CF96C9047AB",
                table: "Voucher",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Voucher__D596F96AE34F7B2C",
                table: "Voucher",
                column: "ticket_id",
                unique: true);

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
            migrationBuilder.DropTable(
                name: "Abonnement");

            migrationBuilder.DropTable(
                name: "Voucher");

            migrationBuilder.DropTable(
                name: "Ticket");

            migrationBuilder.DropTable(
                name: "Match");

            migrationBuilder.DropTable(
                name: "Orderline");

            migrationBuilder.DropTable(
                name: "Zitplaats");

            migrationBuilder.DropTable(
                name: "Club");

            migrationBuilder.DropTable(
                name: "Competitie");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Stadionvak");

            migrationBuilder.DropTable(
                name: "ApplicationUser");

            migrationBuilder.DropTable(
                name: "Stadion");
        }
    }
}
