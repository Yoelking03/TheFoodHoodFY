using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TheFoodHood.Web.Data.Entities
{
    [Table("DetallePedidos")] 
    public class DetallePedido
    {
        [Key]
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public Pedido Pedido { get; set; }
        [ForeignKey("PedidoId")]
        public int ArticuloId { get; set; }
        public Articulo Articulo { get; set; }
        [ForeignKey("ArticuloId")]
        public int Cantidad { get; set; }
    }
}
