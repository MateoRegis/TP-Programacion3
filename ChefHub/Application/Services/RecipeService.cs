using System.Net;
using Application.Interfaces;
using Application.Mappings;
using Application.Models.Request;
using Application.Models.Response;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Application.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRepositoryBase<Recipe> _repositoryBaseRecipe;
        private readonly RecipeMapping _recipeMapping;
        private readonly IRecipeRepository _recipeRepository;

        public RecipeService(IRepositoryBase<Recipe> repositoryBase, RecipeMapping recipeMapping, IRecipeRepository recipeRepository)
        {
            _repositoryBaseRecipe = repositoryBase;
            _recipeMapping = recipeMapping;
            _recipeRepository = recipeRepository;
        }

        public async Task<RecipeResponse> CreateRecipe(RecipeRequest request, int userId)
        {
            var entity = _recipeMapping.FromRequestToEntity(request, userId);
            var response = await _repositoryBaseRecipe.AddAsync(entity);
            var responseMapped = _recipeMapping.FromEntityToResponse(response);

            return responseMapped;
        }

        public async Task ModifyRecipe(RecipeRequest request, int recipeId, int userId)
        {
            var recipeExists = await _recipeRepository.GetRecipeById(userId, recipeId);
            if (recipeExists == null)
            {
                throw new NotFoundException(HttpStatusCode.NotFound, "Receta no encontrada.");
            }

            var recipeMapped = _recipeMapping.FromResponseToEntityMapped(request, recipeExists);
            await _repositoryBaseRecipe.UpdateAsync(recipeMapped);
        }

        public async Task<List<RecipeResponse>> GetRecipesByUser(int userId)
        {
            var response = await _recipeRepository.GetRecipesByUser(userId);
            var responseMapped = response.Select(r => _recipeMapping.FromEntityToResponse(r)).ToList();
            return responseMapped;
        }

    }
}


