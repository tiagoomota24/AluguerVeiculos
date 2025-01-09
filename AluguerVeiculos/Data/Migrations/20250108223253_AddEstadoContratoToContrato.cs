using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AluguerVeiculos.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddEstadoContratoToContrato : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Estado_Contrato",
                table: "Contrato",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado_Contrato",
                table: "Contrato");
        }
    }
}
