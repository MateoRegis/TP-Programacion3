using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class UserRepository : EFRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<User?> GetUserByUserEmail(string email)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
        }
    }
}
