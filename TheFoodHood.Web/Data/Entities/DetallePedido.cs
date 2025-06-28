using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TheFoodHood.Web.Data.Entities;

[Table("DetallePedidos")]
public class DetallePedido
{
    [Key]
    public int Id { get; set; }

    // Clave foránea y navegación al pedido
    public int PedidoId { get; set; }
    public Pedido Pedido { get; set; }
    public int ArticuloId { get; set; }
    public Articulo Articulo { get; set; }

    public int Cantidad { get; set; }
}
