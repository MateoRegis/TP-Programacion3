using Application.Models.Request;
using Application.Models.Response;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappings
{
    public class UserMapping
    {
        public User FromRequestToEntity(UserRequest request)
        {
            return new User
            {
                FullName = request.FullName,
                Email = request.Email,
                Password = request.Password,
                UrlPhoto = request.UrlPhoto,
                Description = request.Description,
                TipoRol = request.TipoRol,
            };

        }
        public UserResponse FromUserToResponse(User entity)
        {
            return new UserResponse
            {
                Id = entity.Id,
                FullName = entity.FullName,
                Email = entity.Email,
                Password = entity.Password,
                UrlPhoto = entity.UrlPhoto,
                Description = entity.Description,
                TipoRol = entity.TipoRol.ToString(),
            };

        }


    }
}
