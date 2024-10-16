using Application.Models.Request;
using Application.Models.Response;
using Domain.Entities;

namespace Application.Mappings
{
    public class RecipeMapping
    {
        public Recipe FromRequestToEntity(RecipeRequest request, int userId)
        {
            return new Recipe
            {
                Title = request.Title,
                Categories = request.Categories,
                Description = request.Description,
                Difficulty = request.Difficulty,
                Ingredients = request.Ingredients,
                Instructions = request.Instructions,
                UrlImage = request.UrlImage,
                PreparationTime = request.PreparationTime,
                UserId = userId,
            };
        }

        public RecipeResponse FromEntityToResponse(Recipe entity)
        {
            var mapping = new CommentMapping();
            return new RecipeResponse
            {
                Id = entity.Id,
                Title = entity.Title,
                Categories = entity.Categories,
                Description = entity.Description,
                Difficulty = entity.Difficulty,
                Ingredients = entity.Ingredients,
                Instructions = entity.Instructions,
                UrlImage = entity.UrlImage,
                PreparationTime = entity.PreparationTime,
                // TODO: Descomentar
                // Comments = entity.ListComments?.Any() == true ? entity.ListComments.Select(s => mapping.FromEntityToResponse(s)).ToList() : new List<CommentResponse>(), 
            };
        }

        public Recipe? FromResponseToEntityMapped(RecipeRequest request, Recipe recipe)
        {
            recipe.Title = request.Title?? recipe.Title;
            recipe.Categories = request.Categories?? recipe.Categories;
            recipe.Description = request.Description?? recipe.Description;
            recipe.Difficulty = request.Difficulty?? recipe.Difficulty;
            recipe.Ingredients = request.Ingredients?? recipe.Ingredients;
            recipe.Instructions = request.Instructions?? recipe.Instructions;
            recipe.UrlImage = request.UrlImage?? recipe.UrlImage;
            recipe.PreparationTime = request.PreparationTime?? recipe.PreparationTime;
            return recipe;
        }
    }
}
