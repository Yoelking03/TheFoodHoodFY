using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheFoodHood.Web.Migrations
{
    /// <inheritdoc />
    public partial class coreciondeDtos11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallePedidos_Articulos_ArticuloId",
                table: "DetallePedidos");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallePedidos_Articulos_ArticuloId1",
                table: "DetallePedidos");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallePedidos_Pedidos_PedidoId1",
                table: "DetallePedidos");

            migrationBuilder.DropIndex(
                name: "IX_DetallePedidos_ArticuloId1",
                table: "DetallePedidos");

            migrationBuilder.DropIndex(
                name: "IX_DetallePedidos_PedidoId1",
                table: "DetallePedidos");

            migrationBuilder.DropColumn(
                name: "Precio",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "ArticuloId1",
                table: "DetallePedidos");

            migrationBuilder.DropColumn(
                name: "PedidoId1",
                table: "DetallePedidos");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallePedidos_Articulos_ArticuloId",
                table: "DetallePedidos",
                column: "ArticuloId",
                principalTable: "Articulos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallePedidos_Articulos_ArticuloId",
                table: "DetallePedidos");

            migrationBuilder.AddColumn<decimal>(
                name: "Precio",
                table: "Pedidos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "ArticuloId1",
                table: "DetallePedidos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PedidoId1",
                table: "DetallePedidos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DetallePedidos_ArticuloId1",
                table: "DetallePedidos",
                column: "ArticuloId1");

            migrationBuilder.CreateIndex(
                name: "IX_DetallePedidos_PedidoId1",
                table: "DetallePedidos",
                column: "PedidoId1");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallePedidos_Articulos_ArticuloId",
                table: "DetallePedidos",
                column: "ArticuloId",
                principalTable: "Articulos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetallePedidos_Articulos_ArticuloId1",
                table: "DetallePedidos",
                column: "ArticuloId1",
                principalTable: "Articulos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallePedidos_Pedidos_PedidoId1",
                table: "DetallePedidos",
                column: "PedidoId1",
                principalTable: "Pedidos",
                principalColumn: "Id");
        }
    }
}
