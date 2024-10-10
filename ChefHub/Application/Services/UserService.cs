using Application.Interfaces;
using Application.Mappings;
using Application.Models.Request;
using Application.Models.Response;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IRepositoryBase<User> _repositoryBase;
        private readonly UserMapping _userMapping;

        public UserService(IRepositoryBase<User> repositoryBase, UserMapping userMapping)
        {
            _repositoryBase = repositoryBase;
            _userMapping = userMapping;
        }

        public async Task<int> CreateUserAsync(UserRequest request)
        {
            var user = _userMapping.FromRequestToEntity(request);
            var entity = await _repositoryBase.AddAsync(user);
            return entity.Id;
        }

        public async Task<UserResponse> GetUserById(int id)
        {
            var entity = await _repositoryBase.GetByIdAsync(id);
            if (entity == null)
            {

                throw new Exception("No se encontro un usuario con este Id");
            }
            var response = _userMapping.FromUserToResponse(entity);
            return response;
        }
    }
}
