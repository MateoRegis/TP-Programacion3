using Application.Models.Request;
using Application.Models.Response;

namespace Application.Interfaces
{
    public interface IRecipeService
    {
        Task<RecipeResponse> CreateRecipe(RecipeRequest request, int userId);
        Task ModifyRecipe(RecipeRequest request, int recipeId, int userId);
        Task<List<RecipeResponse>> GetRecipesByUser(int userId);
    }
}
