using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ptice.Migrations
{
    /// <inheritdoc />
    public partial class V4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vidjenja_Podrucja_PodrucjeID",
                table: "Vidjenja");

            migrationBuilder.DropForeignKey(
                name: "FK_Vidjenja_Ptice_PticaID",
                table: "Vidjenja");

            migrationBuilder.DropColumn(
                name: "BrojVidjenja",
                table: "Vidjenja");

            migrationBuilder.DropColumn(
                name: "Opis",
                table: "Ptice");

            migrationBuilder.AlterColumn<int>(
                name: "PticaID",
                table: "Vidjenja",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PodrucjeID",
                table: "Vidjenja",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "ViseVrednosti",
                table: "Osobine",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Vidjenja_Podrucja_PodrucjeID",
                table: "Vidjenja",
                column: "PodrucjeID",
                principalTable: "Podrucja",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Vidjenja_Ptice_PticaID",
                table: "Vidjenja",
                column: "PticaID",
                principalTable: "Ptice",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vidjenja_Podrucja_PodrucjeID",
                table: "Vidjenja");

            migrationBuilder.DropForeignKey(
                name: "FK_Vidjenja_Ptice_PticaID",
                table: "Vidjenja");

            migrationBuilder.DropColumn(
                name: "ViseVrednosti",
                table: "Osobine");

            migrationBuilder.AlterColumn<int>(
                name: "PticaID",
                table: "Vidjenja",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PodrucjeID",
                table: "Vidjenja",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BrojVidjenja",
                table: "Vidjenja",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Opis",
                table: "Ptice",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Vidjenja_Podrucja_PodrucjeID",
                table: "Vidjenja",
                column: "PodrucjeID",
                principalTable: "Podrucja",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vidjenja_Ptice_PticaID",
                table: "Vidjenja",
                column: "PticaID",
                principalTable: "Ptice",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
