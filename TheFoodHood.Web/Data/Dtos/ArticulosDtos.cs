namespace TheFoodHood.Web.Data.Dtos
{

    public class ArticulosDtos
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public string ImagenUrl { get; set; } = string.Empty;
        public string CategoriaNombre { get; set; } = string.Empty;

      
        public bool Eliminado { get; set; } = false;
    }
}
