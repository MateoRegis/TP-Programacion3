using Application.Models.Request;
using Application.Models.Response;
using Domain.Enum;

namespace Application.Interfaces
{
    public interface IRecipeService
    {
        Task<RecipeResponse> CreateRecipe(RecipeRequest request, int userId);
        Task ModifyRecipe(RecipeRequest request, int recipeId, int userId);
        Task DeleteRecipe(int recipeId, int userId, Role role);
        Task<List<RecipeResponse>> GetRecipesByUser(int userId);
        Task<List<RecipeResponse>> GetAllRecipes();
        Task<RecipeResponse> GetRecipeById(int idRecipe);
    }
}
