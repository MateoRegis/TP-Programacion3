using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Response
{
    public class UserResponse
    {

        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? UrlPhoto { get; set; }
        public string? Description { get; set; }
        public string? Password { get; set; }
        public string? TipoRol { get; set; }
    }
}
