using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace April2024B.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Maratonci",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JMBG = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: true),
                    BrojNagrada = table.Column<int>(type: "int", nullable: false),
                    SrednjaBrzina = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maratonci", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Trke",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Lokacija = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DuzinaStaze = table.Column<double>(type: "float", nullable: false),
                    BrojTakmicara = table.Column<int>(type: "int", nullable: false),
                    TrajanjeTrke = table.Column<TimeSpan>(type: "time", nullable: false),
                    PocetakTrke = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trke", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Ucesca",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaratonacID = table.Column<int>(type: "int", nullable: true),
                    TrkaID = table.Column<int>(type: "int", nullable: true),
                    StartniBroj = table.Column<int>(type: "int", nullable: false),
                    VremeIstrcano = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ucesca", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Ucesca_Maratonci_MaratonacID",
                        column: x => x.MaratonacID,
                        principalTable: "Maratonci",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Ucesca_Trke_TrkaID",
                        column: x => x.TrkaID,
                        principalTable: "Trke",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ucesca_MaratonacID",
                table: "Ucesca",
                column: "MaratonacID");

            migrationBuilder.CreateIndex(
                name: "IX_Ucesca_TrkaID",
                table: "Ucesca",
                column: "TrkaID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ucesca");

            migrationBuilder.DropTable(
                name: "Maratonci");

            migrationBuilder.DropTable(
                name: "Trke");
        }
    }
}
