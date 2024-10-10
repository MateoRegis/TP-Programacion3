using Application.Models.Request;
using Application.Models.Response;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<UserResponse> CreateUserAsync(UserRequest request);
        Task<UserResponse> GetUserById(int id);
        Task<UserResponse> Register(UserRequest request);
    }
}
