using Domain.Entities;
using Domain.Enum;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class FavoriteRepository : EFRepository<Favorite>, IFavoriteRepository
    {
        public FavoriteRepository(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<List<Recipe?>> GetFavoriteRecipesByUserAndType(int userId, FavoriteType favoriteType)
        {
            return await _context.Favorites
                .Where(f => f.UserId == userId && f.FavoriteType == favoriteType) 
                .Include(f => f.Recipe)
                .ThenInclude(r => r.User)
                .Select(f => f.Recipe)                                             
                .ToListAsync();                                                    
        }

        public async Task<List<Favorite>> GetAllUserFavorites(int userId)
        {
            return await _context.Favorites
                .Where(f => f.UserId == userId)
                .Include(f => f.Recipe)
                .ThenInclude(r => r.User)
                .Select(f => f)
                .ToListAsync();
        }
        public async Task<List<Favorite>> GetFavoritesByRecipe(int recipeId)
        {
            return await _context.Favorites.Include(r => r.User).Where(f => f.RecipeId == recipeId).ToListAsync();
        }
    }
}
