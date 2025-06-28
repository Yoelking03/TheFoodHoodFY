using TheFoodHood.Web.Data.Dtos;
using TheFoodHood.Web.Data.Entities;

namespace TheFoodHood.Web.Data.Services
{
    public interface IDetallePedidoService
    {
        List<DetallePedidosDtos> Consultar();
        bool Crear(DetallePedidosDtos detallePedidoDtos);
        bool Eliminar(int id);
        bool Modificar(DetallePedidosDtos detallePedidoDtos);
        DetallePedidosDtos? ObtenerPorPedidoYProducto(int pedidoId, int articuloId);
        List<DetallePedidosDtos> ConsultarPorPedido(int pedidoId);


    }
    public class DetallePedidoService(ApplicationDbContext db) : IDetallePedidoService
    {
        public List<DetallePedidosDtos> Consultar()
        {
            return db.DetallePedidos
                .Select(dp => new DetallePedidosDtos
                {
                    Id = dp.Id,
                    ArticuloId = dp.ArticuloId,
                    PedidoId = dp.PedidoId,
                    Cantidad = dp.Cantidad,

                })
                .ToList();
        }
        public bool Crear(DetallePedidosDtos detallePedidoDtos)
        {
            var detallePedido = new DetallePedido
            {
                ArticuloId = detallePedidoDtos.ArticuloId,
                PedidoId = detallePedidoDtos.PedidoId,
                Cantidad = detallePedidoDtos.Cantidad,

            };
            db.DetallePedidos.Add(detallePedido);
            return db.SaveChanges() > 0;
        }
        private DetallePedido? Consultar(int id)
        {
            return db.DetallePedidos.FirstOrDefault(dp => dp.Id == id);
        }
        public bool Modificar(DetallePedidosDtos detallePedidoDtos)
        {
            var detallePedido = Consultar(detallePedidoDtos.Id);
            if (detallePedido == null) return false;
            detallePedido.ArticuloId = detallePedidoDtos.ArticuloId;
            detallePedido.PedidoId = detallePedidoDtos.PedidoId;
            detallePedido.Cantidad = detallePedidoDtos.Cantidad;
            return db.SaveChanges() > 0;
        }
        public bool Eliminar(int id)
        {
            var detallePedido = Consultar(id);
            if (detallePedido == null) return false;
            db.DetallePedidos.Remove(detallePedido);
            return db.SaveChanges() > 0;
        }
        public DetallePedidosDtos? ObtenerPorPedidoYProducto(int pedidoId, int articuloId)
        {
            var detalle = db.DetallePedidos
                .FirstOrDefault(dp => dp.PedidoId == pedidoId && dp.ArticuloId == articuloId);

            if (detalle == null) return null;

            return new DetallePedidosDtos
            {
                Id = detalle.Id,
                ArticuloId = detalle.ArticuloId,
                PedidoId = detalle.PedidoId,
                Cantidad = detalle.Cantidad
            };
        }
        public List<DetallePedidosDtos> ConsultarPorPedido(int pedidoId)
        {
            return db.DetallePedidos
                .Where(dp => dp.PedidoId == pedidoId)
                .Select(dp => new DetallePedidosDtos
                {
                    Id = dp.Id,
                    ArticuloId = dp.ArticuloId,
                    PedidoId = dp.PedidoId,
                    Cantidad = dp.Cantidad
                })
                .ToList();
        }

    }

}
