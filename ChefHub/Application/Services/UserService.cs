using Application.Interfaces;
using Application.Mappings;
using Application.Models.Request;
using Application.Models.Response;
using Domain.Entities;
using Domain.Enum;
using Domain.Interfaces;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IRepositoryBase<User> _repositoryBase;
        private readonly UserMapping _userMapping;
        private readonly IUserRepository _userRepository;
        public UserService(IRepositoryBase<User> repositoryBase, UserMapping userMapping, IUserRepository userRepository)
        {
            _repositoryBase = repositoryBase;
            _userMapping = userMapping;
            _userRepository = userRepository;
        }

        public async Task<UserResponse> CreateUserAsync(UserRequest request)
        {
            var user = _userMapping.FromRequestToEntity(request);
            var entity = await _repositoryBase.AddAsync(user);
            return _userMapping.FromUserToResponse(entity);
        }

        public async Task<UserResponse?> GetUserById(int id)
        {
            var entity = await _repositoryBase.GetByIdAsync(id);
            if (entity == null)
            {
                return null;
            }
            var response = _userMapping.FromUserToResponse(entity);
            return response;
        }

        public async Task<UserResponse?> Register(UserRequest request)
        {
            var exist = await _userRepository.GetUserByUserEmail(request.Email);
            if(exist != null)
            {
                return null;
            }
            request.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
            var user = _userMapping.FromRequestToEntity(request);
            user.TipoRol = Rol.Common;
            var response = await _repositoryBase.AddAsync(user);
            var responseMapped = _userMapping.FromUserToResponse(response);
            return responseMapped;
        }
    }
}
