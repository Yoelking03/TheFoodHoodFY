using System.Collections.Generic;
using TheFoodHood.Web.Data.Dtos;

namespace TheFoodHood.Web.Data.Entities
{
    public static class PedidoTemporalStore
    {
        // Diccionario estático que guarda pedidos en memoria por usuario (UsuarioId)
        public static Dictionary<string, PedidosDtos> PedidosPorUsuario { get; set; } = new();
    }
}
