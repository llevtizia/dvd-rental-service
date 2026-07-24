using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dvd_rental.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clienti",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Cognome = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clienti", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dvds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Titolo = table.Column<string>(type: "TEXT", nullable: false),
                    DataUscita = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    Categoria = table.Column<string>(type: "TEXT", nullable: false),
                    DurataMinuti = table.Column<int>(type: "INTEGER", nullable: false),
                    QuantitaTotale = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dvds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Noleggi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClienteId = table.Column<int>(type: "INTEGER", nullable: false),
                    DvdId = table.Column<int>(type: "INTEGER", nullable: false),
                    DataNoleggio = table.Column<DateTime>(type: "TEXT", nullable: false),
                    RestituzionePrevista = table.Column<DateTime>(type: "TEXT", nullable: false),
                    RestituzioneEffettiva = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Noleggi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Noleggi_Clienti_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clienti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Noleggi_Dvds_DvdId",
                        column: x => x.DvdId,
                        principalTable: "Dvds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Noleggi_ClienteId",
                table: "Noleggi",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Noleggi_DvdId",
                table: "Noleggi",
                column: "DvdId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Noleggi");

            migrationBuilder.DropTable(
                name: "Clienti");

            migrationBuilder.DropTable(
                name: "Dvds");
        }
    }
}
