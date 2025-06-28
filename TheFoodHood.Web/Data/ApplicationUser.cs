using Microsoft.AspNetCore.Identity;
using TheFoodHood.Web.Data.Entities;

namespace TheFoodHood.Web.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string TipoUsuario { get; set; }
    }

}
