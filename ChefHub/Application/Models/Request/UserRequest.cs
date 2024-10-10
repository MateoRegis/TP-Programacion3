using Domain.Enum;

namespace Application.Models.Request
{
    public class UserRequest
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? UrlPhoto { get; set; }
        public string? Description { get; set; }
        public string? Password { get; set; }
        public Rol TipoRol { get; set; }
    }
}
