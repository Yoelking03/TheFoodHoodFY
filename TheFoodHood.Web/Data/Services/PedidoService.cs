using Microsoft.EntityFrameworkCore;
using TheFoodHood.Web.Data.Dtos;
using TheFoodHood.Web.Data.Entities;

namespace TheFoodHood.Web.Data.Services
{
    public interface IPedidoService
    {
        List<PedidosDtos> Consultar();
        bool Crear(PedidosDtos pedidoDtos);
        bool Eliminar(int id);
        bool Modificar(PedidosDtos pedidosDtos);
        PedidosDtos? ObtenerPendientePorUsuario(string usuarioId);
    }

    public class PedidoService(ApplicationDbContext db) : IPedidoService
    {
        public List<PedidosDtos> Consultar()
        {
            return db.Pedidos
                .Include(p => p.Usuario)
                .Select(p => new PedidosDtos
                {
                    Id = p.Id,
                    UsuarioId = p.UsuarioId,
                    FechaPedido = p.Fecha,
                    Total = p.Total,
                    Estado = p.Estado,
                    Delivery = p.Delivery,
                    Direccion = p.Direccion,
                    TipoEntrega = p.TipoEntrega,
                    Detalles = db.DetallePedidos
                        .Where(d => d.PedidoId == p.Id)
                        .Select(d => new DetallePedidosDtos
                        {
                            Id = d.Id,
                            PedidoId = d.PedidoId,
                            ArticuloId = d.ArticuloId,
                            Cantidad = d.Cantidad
                        }).ToList()
                })
                .ToList();
        }

        public bool Crear(PedidosDtos pedidoDtos)
        {
            var pedido = new Pedido
            {
                UsuarioId = pedidoDtos.UsuarioId,
                Fecha = pedidoDtos.FechaPedido,
                Total = pedidoDtos.Total,
                Estado = pedidoDtos.Estado,
                Delivery = pedidoDtos.Delivery,
                Direccion = pedidoDtos.Direccion,
                TipoEntrega = pedidoDtos.TipoEntrega
            };
            db.Pedidos.Add(pedido);
            return db.SaveChanges() > 0;
        }

        private Pedido? Consultar(int id)
        {
            return db.Pedidos.FirstOrDefault(p => p.Id == id);
        }

        public bool Modificar(PedidosDtos pedidosDtos)
        {
            var pedido = Consultar(pedidosDtos.Id);
            if (pedido == null) return false;

            pedido.UsuarioId = pedidosDtos.UsuarioId;
            pedido.Fecha = pedidosDtos.FechaPedido;
            pedido.Total = pedidosDtos.Total;
            pedido.Estado = pedidosDtos.Estado;
            pedido.Delivery = pedidosDtos.Delivery;
            pedido.Direccion = pedidosDtos.Direccion;
            pedido.TipoEntrega = pedidosDtos.TipoEntrega;

            return db.SaveChanges() > 0;
        }

        public bool Eliminar(int id)
        {
            var pedido = Consultar(id);
            if (pedido == null) return false;
            db.Pedidos.Remove(pedido);
            return db.SaveChanges() > 0;
        }

        public PedidosDtos? ObtenerPendientePorUsuario(string usuarioId)
        {
            var pedido = db.Pedidos
                .Include(p => p.Usuario)
                .FirstOrDefault(p => p.UsuarioId == usuarioId && p.Estado == "pendiente");

            if (pedido == null) return null;

            var detalles = db.DetallePedidos
                .Where(d => d.PedidoId == pedido.Id)
                .Select(d => new DetallePedidosDtos
                {
                    Id = d.Id,
                    PedidoId = d.PedidoId,
                    ArticuloId = d.ArticuloId,
                    Cantidad = d.Cantidad
                }).ToList();

            return new PedidosDtos
            {
                Id = pedido.Id,
                UsuarioId = pedido.UsuarioId,
                FechaPedido = pedido.Fecha,
                Total = pedido.Total,
                Estado = pedido.Estado,
                Delivery = pedido.Delivery,
                Direccion = pedido.Direccion,
                TipoEntrega = pedido.TipoEntrega,
                Detalles = detalles
            };
        }
    }
}
