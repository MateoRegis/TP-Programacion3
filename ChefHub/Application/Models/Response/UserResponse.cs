namespace Application.Models.Response
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? UrlPhoto { get; set; }
        public string? Description { get; set; }
        public string? TipoRol { get; set; }
    }
}
