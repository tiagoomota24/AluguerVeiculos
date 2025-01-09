using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AluguerVeiculos.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddQuilometragemFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quilometragem",
                table: "Veiculos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QuilometragemFinal",
                table: "Contrato",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quilometragem",
                table: "Veiculos");

            migrationBuilder.DropColumn(
                name: "QuilometragemFinal",
                table: "Contrato");
        }
    }
}
