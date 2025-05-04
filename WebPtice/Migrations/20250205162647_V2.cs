using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ptice.Migrations
{
    /// <inheritdoc />
    public partial class V2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NepoznataPticaOsobine_NepoznatePtice_NepoznataID",
                table: "NepoznataPticaOsobine");

            migrationBuilder.DropTable(
                name: "Pozicije");

            migrationBuilder.DropTable(
                name: "Veze");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NepoznatePtice",
                table: "NepoznatePtice");

            migrationBuilder.RenameTable(
                name: "NepoznatePtice",
                newName: "Nepoznata");

            migrationBuilder.AlterColumn<string>(
                name: "Naziv",
                table: "Ptice",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Opis",
                table: "Ptice",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Slika",
                table: "Ptice",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Nepoznata",
                table: "Nepoznata",
                column: "ID");

            migrationBuilder.CreateTable(
                name: "Vidjenja",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrojVidjenja = table.Column<int>(type: "int", nullable: false),
                    Vreme = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    PticaID = table.Column<int>(type: "int", nullable: false),
                    PodrucjeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vidjenja", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Vidjenja_Podrucja_PodrucjeID",
                        column: x => x.PodrucjeID,
                        principalTable: "Podrucja",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vidjenja_Ptice_PticaID",
                        column: x => x.PticaID,
                        principalTable: "Ptice",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vidjenja_PodrucjeID",
                table: "Vidjenja",
                column: "PodrucjeID");

            migrationBuilder.CreateIndex(
                name: "IX_Vidjenja_PticaID",
                table: "Vidjenja",
                column: "PticaID");

            migrationBuilder.AddForeignKey(
                name: "FK_NepoznataPticaOsobine_Nepoznata_NepoznataID",
                table: "NepoznataPticaOsobine",
                column: "NepoznataID",
                principalTable: "Nepoznata",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NepoznataPticaOsobine_Nepoznata_NepoznataID",
                table: "NepoznataPticaOsobine");

            migrationBuilder.DropTable(
                name: "Vidjenja");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Nepoznata",
                table: "Nepoznata");

            migrationBuilder.DropColumn(
                name: "Opis",
                table: "Ptice");

            migrationBuilder.DropColumn(
                name: "Slika",
                table: "Ptice");

            migrationBuilder.RenameTable(
                name: "Nepoznata",
                newName: "NepoznatePtice");

            migrationBuilder.AlterColumn<string>(
                name: "Naziv",
                table: "Ptice",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddPrimaryKey(
                name: "PK_NepoznatePtice",
                table: "NepoznatePtice",
                column: "ID");

            migrationBuilder.CreateTable(
                name: "Veze",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PodrucjeID = table.Column<int>(type: "int", nullable: false),
                    PticaID = table.Column<int>(type: "int", nullable: false),
                    Brojac = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Veze", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Veze_Podrucja_PodrucjeID",
                        column: x => x.PodrucjeID,
                        principalTable: "Podrucja",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Veze_Ptice_PticaID",
                        column: x => x.PticaID,
                        principalTable: "Ptice",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pozicije",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VezaID = table.Column<int>(type: "int", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pozicije", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Pozicije_Veze_VezaID",
                        column: x => x.VezaID,
                        principalTable: "Veze",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pozicije_VezaID",
                table: "Pozicije",
                column: "VezaID");

            migrationBuilder.CreateIndex(
                name: "IX_Veze_PodrucjeID",
                table: "Veze",
                column: "PodrucjeID");

            migrationBuilder.CreateIndex(
                name: "IX_Veze_PticaID",
                table: "Veze",
                column: "PticaID");

            migrationBuilder.AddForeignKey(
                name: "FK_NepoznataPticaOsobine_NepoznatePtice_NepoznataID",
                table: "NepoznataPticaOsobine",
                column: "NepoznataID",
                principalTable: "NepoznatePtice",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
