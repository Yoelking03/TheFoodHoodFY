using TheFoodHood.Web.Data.Dtos;
using TheFoodHood.Web.Data.Entities;

namespace TheFoodHood.Web.Data.Services
{
    public interface IArticuloService
    {
        List<ArticulosDtos> Consultar();
        bool Crear(ArticulosDtos articuloDtos);
        bool Eliminar(int id);
        bool Modificar(ArticulosDtos articulosDtos);
        ArticulosDtos? ConsultarPorId(int id);
    }

    public class ArticuloService(ApplicationDbContext db) : IArticuloService
    {
        public List<ArticulosDtos> Consultar()
        {
            return db.Articulos
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
        public bool Crear(ArticulosDtos articuloDtos)
        {
            var articulo = new Articulo
            {
                Nombre = articuloDtos.Nombre,
                Descripcion = articuloDtos.Descripcion,
                Precio = articuloDtos.Precio,
                ImagenUrl = articuloDtos.ImagenUrl,
                CategoriaNombre = articuloDtos.CategoriaNombre
            };

            db.Articulos.Add(articulo);
            return db.SaveChanges() > 0;
            }

        public Articulo? Consultar(int id)
        {
            var articulo = db.Articulos.FirstOrDefault(a => a.Id == id);
            return articulo;
        }
        public bool Modificar(ArticulosDtos articulosDtos)
        {
            var articulo = Consultar(articulosDtos.Id);
            if (articulo == null) return false;
            articulo.Nombre = articulosDtos.Nombre;
            articulo.Descripcion = articulosDtos.Descripcion;
            articulo.Precio = articulosDtos.Precio;
            articulo.ImagenUrl = articulosDtos.ImagenUrl;
            articulo.CategoriaNombre = articulosDtos.CategoriaNombre;
            return db.SaveChanges() > 0;
        }
        public bool Eliminar(int id)
        {
            var articulo = Consultar(id);
            if (articulo == null) return false;
            db.Articulos.Remove(articulo);
            return db.SaveChanges() > 0;
        }
        public ArticulosDtos? ConsultarPorId(int id)
        {
            var articulo = db.Articulos.FirstOrDefault(a => a.Id == id);
            if (articulo == null) return null;

            return new ArticulosDtos
            {
                Id = articulo.Id,
                Nombre = articulo.Nombre,
                Descripcion = articulo.Descripcion,
                Precio = articulo.Precio,
                ImagenUrl = articulo.ImagenUrl,
                CategoriaNombre = articulo.CategoriaNombre
            };
        }



    }
}



