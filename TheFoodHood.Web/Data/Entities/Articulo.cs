using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheFoodHood.Web.Data.Entities
{
    [Table("Articulos")]
    public class Articulo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(120)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [MaxLength(500)]
        public string Descripcion { get; set; } = string.Empty;

        [Column(TypeName = "decimal(10,2)")]
        public decimal Precio { get; set; }

        [Required]
        public string ImagenUrl { get; set; } = string.Empty;

        [Required]
        [MaxLength(60)]
        public string CategoriaNombre { get; set; } = string.Empty;

        /* >>> bandera de soft-delete <<< */
        public bool Eliminado { get; set; } = false;

        /* navegación */
        public List<DetallePedido>? DetallesPedido { get; set; }
    }
}
