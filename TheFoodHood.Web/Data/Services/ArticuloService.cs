using Microsoft.EntityFrameworkCore;
using TheFoodHood.Web.Data.Dtos;
using TheFoodHood.Web.Data.Entities;

namespace TheFoodHood.Web.Data.Services
{
    public interface IArticuloService
    {
        /* CRUD + soft-delete */
        List<ArticulosDtos> Consultar();
        ArticulosDtos? ConsultarPorId(int id);
        bool Crear(ArticulosDtos dto);
        bool Modificar(ArticulosDtos dto);
        bool Archivar(int id);   // ⇠ marca Eliminado = true
        bool Restaurar(int id);  // opcional, para revertir
    }

    public class ArticuloService : IArticuloService
    {
        private readonly ApplicationDbContext _db;
        public ArticuloService(ApplicationDbContext db) => _db = db;

        /* ========================== SELECT ========================== */
        public List<ArticulosDtos> Consultar()
        {
            return _db.Articulos
                      .AsNoTracking()
                      .Where(a => !a.Eliminado)              // solo activos
                      .Select(a => new ArticulosDtos
                      {
                          Id = a.Id,
                          Nombre = a.Nombre,
                          Descripcion = a.Descripcion,
                          Precio = a.Precio,
                          ImagenUrl = a.ImagenUrl,
                          CategoriaNombre = a.CategoriaNombre
                      })
                      .ToList();
        }

        public ArticulosDtos? ConsultarPorId(int id)
        {
            var a = _db.Articulos.AsNoTracking()
                                 .FirstOrDefault(x => x.Id == id && !x.Eliminado);
            if (a is null) return null;

            return new ArticulosDtos
            {
                Id = a.Id,
                Nombre = a.Nombre,
                Descripcion = a.Descripcion,
                Precio = a.Precio,
                ImagenUrl = a.ImagenUrl,
                CategoriaNombre = a.CategoriaNombre
            };
        }

        /* =========================== INSERT ========================= */
        public bool Crear(ArticulosDtos dto)
        {
            var articulo = new Articulo
            {
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                Precio = dto.Precio,
                ImagenUrl = dto.ImagenUrl,
                CategoriaNombre = dto.CategoriaNombre,
                Eliminado = false
            };

            _db.Articulos.Add(articulo);
            return _db.SaveChanges() > 0;
        }

        /* =========================== UPDATE ========================= */
        public bool Modificar(ArticulosDtos dto)
        {
            var art = _db.Articulos.FirstOrDefault(a => a.Id == dto.Id && !a.Eliminado);
            if (art is null) return false;

            art.Nombre = dto.Nombre;
            art.Descripcion = dto.Descripcion;
            art.Precio = dto.Precio;
            art.ImagenUrl = dto.ImagenUrl;
            art.CategoriaNombre = dto.CategoriaNombre;

            return _db.SaveChanges() > 0;
        }

        /* ====================== SOFT-DELETE / RESTORE =============== */
        public bool Archivar(int id)
        {
            var art = _db.Articulos.FirstOrDefault(a => a.Id == id);
            if (art is null) return false;

            art.Eliminado = true;
            return _db.SaveChanges() > 0;
        }

        public bool Restaurar(int id)
        {
            var art = _db.Articulos.FirstOrDefault(a => a.Id == id);
            if (art is null) return false;

            art.Eliminado = false;
            return _db.SaveChanges() > 0;
        }
    }
}
