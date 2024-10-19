using System.Net;
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
    public class FavoriteService : IFavoriteService
    {
        private readonly IRepositoryBase<Favorite> _repositoryBaseFavorite;
        private readonly IRepositoryBase<User> _repositoryBaseUser;
        private readonly IRepositoryBase<Recipe> _repositoryBaseRecipe;
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly FavoriteMapping _favoriteMapping;
        private readonly RecipeMapping _recipeMapping;
        public FavoriteService(IRepositoryBase<Favorite> repositoryBaseFavorite, IRepositoryBase<User> repositoryBaseUser, IRepositoryBase<Recipe> repositoryBaseRecipe, IFavoriteRepository favoriteRepository, FavoriteMapping favoriteMapping, RecipeMapping recipeMapping)
        {
            _repositoryBaseFavorite = repositoryBaseFavorite;
            _repositoryBaseUser = repositoryBaseUser;
            _repositoryBaseRecipe = repositoryBaseRecipe;
            _favoriteRepository = favoriteRepository;
            _favoriteMapping = favoriteMapping;
            _recipeMapping = recipeMapping;
        }

        public async Task AddToFavorites(int userId, FavoriteRequest favoriteRequest)
        {
            var recipeExist = await _repositoryBaseRecipe.EntityExistsAsync(favoriteRequest.RecipeId);
            if (!recipeExist)
            {
                throw new NotFoundException(HttpStatusCode.NotFound, "Receta no encontrada.");
            };

            var userExists = await _repositoryBaseUser.EntityExistsAsync(userId);
            if (!userExists)
            {
                throw new NotFoundException(HttpStatusCode.NotFound, "Usuario no encontrado.");
            };

            var favoriteExists = Enum.IsDefined(typeof(FavoriteType), favoriteRequest.FavoriteType);
            if (!favoriteExists)
            {
                throw new NotFoundException(HttpStatusCode.NotFound, "Tipo de favorito no encontrado.");
            };

            var entity = _favoriteMapping.FromRequestToEntity(userId, favoriteRequest);
            await _repositoryBaseFavorite.AddAsync(entity);
        }

        public async Task DeleteFavorite(int userId, int favoriteId)
        {
            var favoriteExists = await _repositoryBaseFavorite.GetByIdAsync(favoriteId);
            if (favoriteExists == null)
            {
                throw new NotFoundException(HttpStatusCode.NotFound, "Favorito no encontrado.");
            }
            if (favoriteExists.UserId != userId)
            {
                throw new NotAllowedException(HttpStatusCode.Forbidden, "Favorito no pertenece al usuario");
            }
            await _repositoryBaseFavorite.DeleteAsync(favoriteExists);
        }

        public async Task ModifyFavorite(int userId, int favoriteId, FavoriteRequest favoriteRequest)
        {
            var favoriteExists = await _repositoryBaseFavorite.GetByIdAsync(favoriteId);
            if (favoriteExists == null)
            {
                throw new NotFoundException(HttpStatusCode.NotFound, "Favorito no encontrado.");
            }
            if (favoriteExists.UserId != userId)
            {
                throw new NotAllowedException(HttpStatusCode.Forbidden, "Favorito no pertenece al usuario");
            }
            favoriteExists.FavoriteType = favoriteRequest.FavoriteType;
            await _repositoryBaseFavorite.UpdateAsync(favoriteExists);
        }

        public async Task<List<RecipeResponse>> GetFavoritesByUserAndType(int userId, FavoriteType favoriteType)
        {
            var favorite = await _favoriteRepository.GetFavoriteRecipesByUserAndType(userId, favoriteType);
            var response = favorite.Select(f => _recipeMapping.FromEntityToResponse(f)).ToList();
            return response;
        }

        public async Task<List<FavoriteResponse>> GetAllUserFavorites(int userId)
        {
            var favorite = await _favoriteRepository.GetAllUserFavorites(userId);
            var response = favorite.Select(f => _favoriteMapping.FromEntityToResponse(f)).ToList();
            return response;
        }
    }
}
