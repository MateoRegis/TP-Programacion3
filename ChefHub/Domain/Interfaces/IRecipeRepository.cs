using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IRecipeRepository
    {
        Task<List<Recipe>> GetRecipesByUser(int userId);
        Task<Recipe?> GetRecipeById(int userId, int recipeId);
        Task<List<Recipe>> GetAllRecipes();
        Task<Recipe?> GetRecipeById(int recipeId);
    }
}
