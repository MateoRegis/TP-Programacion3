using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserByUserEmail(string email);
    }
}
