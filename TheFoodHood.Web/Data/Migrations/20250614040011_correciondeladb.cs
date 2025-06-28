using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheFoodHood.Web.Migrations
{
    /// <inheritdoc />
    public partial class correciondeladb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
