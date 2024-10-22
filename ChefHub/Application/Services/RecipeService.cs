using System.Net;
using System.Security.Principal;
using Application.Interfaces;
using Application.Mappings;
using Application.Models.Request;
using Application.Models.Response;
using Domain.Entities;
using Domain.Enum;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Application.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRepositoryBase<Recipe> _repositoryBaseRecipe;
        private readonly RecipeMapping _recipeMapping;
        private readonly IRecipeRepository _recipeRepository;
        private readonly IRepositoryBase<Favorite> _repositoryBaseFavorite;
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly IRepositoryBase<Comment> _repositoryBaseComment;
        private readonly ICommentRepository _commentRepository;

        public RecipeService(IRepositoryBase<Recipe> repositoryBase, RecipeMapping recipeMapping, IRecipeRepository recipeRepository, IRepositoryBase<Favorite> repositoryBaseFavorite, IFavoriteRepository favoriteRepository, IRepositoryBase<Comment> repositoryBaseComment, ICommentRepository commentRepository)
        {
            _repositoryBaseRecipe = repositoryBase;
            _recipeMapping = recipeMapping;
            _recipeRepository = recipeRepository;
            _repositoryBaseFavorite = repositoryBaseFavorite;
            _favoriteRepository = favoriteRepository;
            _repositoryBaseComment = repositoryBaseComment;
            _commentRepository = commentRepository;
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

            var recipeMapped = _recipeMapping.FromEntityToEntityUpdated(request, recipeExists);
            await _repositoryBaseRecipe.UpdateAsync(recipeMapped);
        }

        public async Task DeleteRecipe(int recipeId, int userId, Role role)
        {   
            var recipeExists = await _repositoryBaseRecipe.GetByIdAsync(recipeId);
            if (recipeExists == null)
            {
                throw new NotFoundException(HttpStatusCode.NotFound, "Receta no encontrada.");
            }

            if (role == Role.Common)
            {
                if (recipeExists.UserId != userId)
                {
                    throw new NotAllowedException(HttpStatusCode.Forbidden, "Receta no pertenece al usuario");
                }
            }

            var favorites = await _favoriteRepository.GetFavoritesByRecipe(recipeId);
            foreach (var favorite in favorites)
            {
                await _repositoryBaseFavorite.DeleteAsync(favorite);
            }

            var comments = await _commentRepository.GetCommentsByRecipe(recipeId);
            foreach (var comment in comments)
            {
                await _repositoryBaseComment.DeleteAsync(comment);
            }

            await _repositoryBaseRecipe.DeleteAsync(recipeExists);
        }

        public async Task<List<RecipeResponse>> GetRecipesByUser(int userId)
        {
            var response = await _recipeRepository.GetRecipesByUser(userId);
            var responseMapped = response.Select(r => _recipeMapping.FromEntityToResponse(r)).ToList();
            return responseMapped;
        }

        public async Task<List<RecipeResponse>> GetAllRecipes()
        {
            var response = await _recipeRepository.GetAllRecipes();
            var responseMapped = response.Select(r => _recipeMapping.FromEntityToResponse(r)).ToList();
            return responseMapped;
        }
        public async Task<RecipeResponse> GetRecipeById(int idRecipe)
        {
            var response = await _recipeRepository.GetRecipeById(idRecipe);
            if (response == null)
            {
                throw new NotFoundException(HttpStatusCode.NotFound, "No Se Encontro La Receta");
            }
            var responseMapped = _recipeMapping.FromEntityToResponse(response);
            return responseMapped;
        }
    }
}


