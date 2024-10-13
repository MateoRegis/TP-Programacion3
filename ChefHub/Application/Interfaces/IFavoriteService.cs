using Application.Models.Response;
using Domain.Enum;

namespace Application.Interfaces
{
    public interface IFavoriteService
    {
        Task AddToFavorites(int userId, int recipeId, FavoriteType favoriteType);  
        Task<List<RecipeResponse>> GetFavoritesByUserAndType(int userId, FavoriteType favoriteType);
    }
}
