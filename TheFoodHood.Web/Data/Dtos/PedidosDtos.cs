namespace TheFoodHood.Web.Data.Dtos
{
    public class PedidosDtos
    {
        public int Id { get; set; }
        public string UsuarioId { get; set; } = string.Empty;
        public DateTime FechaPedido { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; } = string.Empty;
        public string Delivery { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string TipoEntrega { get; set; } = string.Empty;

        public List<DetallePedidosDtos> Detalles { get; set; } = new();
    }
}
