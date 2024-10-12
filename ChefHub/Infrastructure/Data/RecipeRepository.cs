using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class RecipeRepository : EFRepository<Recipe>, IRecipeRepository
    {
        public RecipeRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<Recipe>> GetRecipesByUser(int userId)
        {
            var recipes = await _context.Recipes.Include(r => r.ListComments).ThenInclude(c => c.User).Where(r => r.UserId == userId).ToListAsync();

            return recipes;

        }
    }

}
