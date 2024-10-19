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

        public async Task<List<Recipe>> GetFavoriteRecipesByUserAndType(int userId, FavoriteType favoriteType)
        {
            return await _context.Favorites
                .Where(f => f.UserId == userId && f.FavoriteType == favoriteType)   // Filtrar por UserId y FavoriteType
                .Include(f => f.Recipe)                                             // Incluir la relación con Recipe
                .Select(f => f.Recipe)                                              // Seleccionar solo las recetas
                .ToListAsync();                                                     // Obtener la lista de recetas
        }

        public async Task<List<Favorite>> GetAllUserFavorites(int userId)
        {
            return await _context.Favorites
                .Where(f => f.UserId == userId)
                .Include(f => f.Recipe)
                .Select(f => f)
                .ToListAsync();
        }
    }
}
