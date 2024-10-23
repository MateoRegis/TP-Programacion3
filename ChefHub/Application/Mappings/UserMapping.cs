using Application.Models.Request;
using Application.Models.Response;
using Domain.Entities;

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
                UrlPhoto = entity.UrlPhoto,
                Description = entity.Description,
                TipoRol = entity.TipoRol.ToString(),
            }; 
        }

        public User? FromEntityToEntityUpdated(UserRequest request, User entity)
        {
            entity.FullName = request.FullName ?? entity.FullName;
            entity.Email = request.Email ?? entity.Email;
            entity.UrlPhoto = request.UrlPhoto ?? entity.UrlPhoto;
            entity.Description = request.Description ?? entity.Description;
            return entity;
        }
    }
}
