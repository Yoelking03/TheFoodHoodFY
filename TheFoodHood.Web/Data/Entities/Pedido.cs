using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheFoodHood.Web.Data.Entities
{
    [Table("Pedidos")]
    public class Pedido
    {
        [Key]
        public int Id { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;

        public string UsuarioId { get; set; } = string.Empty;
        public ApplicationUser Usuario { get; set; }

        public decimal Total { get; set; }
        public string Delivery { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string TipoEntrega { get; set; } = string.Empty;
        public string Estado { get; set; } = "Pendiente";

        public List<DetallePedido>? Detalles { get; set; }
    }
}
