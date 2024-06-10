using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAuteurLivresRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Livres_Auteurs_AuteurID",
                table: "Livres");

            migrationBuilder.RenameColumn(
                name: "AuteurID",
                table: "Livres",
                newName: "AuteurId");

            migrationBuilder.RenameIndex(
                name: "IX_Livres_AuteurID",
                table: "Livres",
                newName: "IX_Livres_AuteurId");

            migrationBuilder.AlterColumn<string>(
                name: "Résumé",
                table: "Livres",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Maisondédition",
                table: "Livres",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Prenom",
                table: "Auteurs",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Nom",
                table: "Auteurs",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_Livres_Auteurs_AuteurId",
                table: "Livres",
                column: "AuteurId",
                principalTable: "Auteurs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Livres_Auteurs_AuteurId",
                table: "Livres");

            migrationBuilder.RenameColumn(
                name: "AuteurId",
                table: "Livres",
                newName: "AuteurID");

            migrationBuilder.RenameIndex(
                name: "IX_Livres_AuteurId",
                table: "Livres",
                newName: "IX_Livres_AuteurID");

            migrationBuilder.AlterColumn<string>(
                name: "Résumé",
                table: "Livres",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Maisondédition",
                table: "Livres",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Prenom",
                table: "Auteurs",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nom",
                table: "Auteurs",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Livres_Auteurs_AuteurID",
                table: "Livres",
                column: "AuteurID",
                principalTable: "Auteurs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
