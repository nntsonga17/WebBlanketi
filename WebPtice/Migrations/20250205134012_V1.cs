using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ptice.Migrations
{
    /// <inheritdoc />
    public partial class V1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NepoznatePtice",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NepoznatePtice", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Osobine",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Vrednost = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Osobine", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Podrucja",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Podrucja", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Ptice",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ptice", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NepoznataPticaOsobine",
                columns: table => new
                {
                    NepoznataID = table.Column<int>(type: "int", nullable: false),
                    OsobineID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NepoznataPticaOsobine", x => new { x.NepoznataID, x.OsobineID });
                    table.ForeignKey(
                        name: "FK_NepoznataPticaOsobine_NepoznatePtice_NepoznataID",
                        column: x => x.NepoznataID,
                        principalTable: "NepoznatePtice",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NepoznataPticaOsobine_Osobine_OsobineID",
                        column: x => x.OsobineID,
                        principalTable: "Osobine",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OsobinePtica",
                columns: table => new
                {
                    OsobineID = table.Column<int>(type: "int", nullable: false),
                    PticaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OsobinePtica", x => new { x.OsobineID, x.PticaID });
                    table.ForeignKey(
                        name: "FK_OsobinePtica_Osobine_OsobineID",
                        column: x => x.OsobineID,
                        principalTable: "Osobine",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OsobinePtica_Ptice_PticaID",
                        column: x => x.PticaID,
                        principalTable: "Ptice",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Veze",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brojac = table.Column<int>(type: "int", nullable: false),
                    PodrucjeID = table.Column<int>(type: "int", nullable: false),
                    PticaID = table.Column<int>(type: "int", nullable: false)
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
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    VezaID = table.Column<int>(type: "int", nullable: false)
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
                name: "IX_NepoznataPticaOsobine_OsobineID",
                table: "NepoznataPticaOsobine",
                column: "OsobineID");

            migrationBuilder.CreateIndex(
                name: "IX_OsobinePtica_PticaID",
                table: "OsobinePtica",
                column: "PticaID");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NepoznataPticaOsobine");

            migrationBuilder.DropTable(
                name: "OsobinePtica");

            migrationBuilder.DropTable(
                name: "Pozicije");

            migrationBuilder.DropTable(
                name: "NepoznatePtice");

            migrationBuilder.DropTable(
                name: "Osobine");

            migrationBuilder.DropTable(
                name: "Veze");

            migrationBuilder.DropTable(
                name: "Podrucja");

            migrationBuilder.DropTable(
                name: "Ptice");
        }
    }
}
