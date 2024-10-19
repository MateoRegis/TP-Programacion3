using Domain.Entities;
using Domain.Enum;

namespace Domain.Interfaces
{
    public interface IFavoriteRepository
    {
        Task<List<Recipe>> GetFavoriteRecipesByUserAndType(int userId, FavoriteType favoriteType);
        Task<List<Favorite>> GetAllUserFavorites(int userId);
        Task<List<Favorite>> GetFavoritesByRecipe(int recipeId);
    }
}
