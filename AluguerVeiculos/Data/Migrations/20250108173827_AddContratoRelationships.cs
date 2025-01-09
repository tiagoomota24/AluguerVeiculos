using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AluguerVeiculos.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddContratoRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contrato_Clientes_ClienteId",
                table: "Contrato");

            migrationBuilder.DropForeignKey(
                name: "FK_Contrato_Veiculos_VeiculoId",
                table: "Contrato");

            migrationBuilder.AddForeignKey(
                name: "FK_Contrato_Clientes_ClienteId",
                table: "Contrato",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contrato_Veiculos_VeiculoId",
                table: "Contrato",
                column: "VeiculoId",
                principalTable: "Veiculos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contrato_Clientes_ClienteId",
                table: "Contrato");

            migrationBuilder.DropForeignKey(
                name: "FK_Contrato_Veiculos_VeiculoId",
                table: "Contrato");

            migrationBuilder.AddForeignKey(
                name: "FK_Contrato_Clientes_ClienteId",
                table: "Contrato",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contrato_Veiculos_VeiculoId",
                table: "Contrato",
                column: "VeiculoId",
                principalTable: "Veiculos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
