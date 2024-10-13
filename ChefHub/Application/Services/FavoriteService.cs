using System.Net;
using Application.Interfaces;
using Application.Models.Response;
using Domain.Entities;
using Domain.Enum;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Application.Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly IFavoriteService _favoriteService;
        private readonly IRepositoryBase<Favorite> _repositoryBaseFavorite;
        private readonly IRepositoryBase<User> _repositoryBaseUser;
        private readonly IFavoriteRepository _favoriteRepository;
        public FavoriteService(IFavoriteService favoriteService, IRepositoryBase<Favorite> repositoryBaseFavorite, IRepositoryBase<User> repositoryBaseUser, IFavoriteRepository favoriteRepository)
        {
            _favoriteService = favoriteService;
            _repositoryBaseFavorite = repositoryBaseFavorite;
            _repositoryBaseUser = repositoryBaseUser;
            _favoriteRepository = favoriteRepository;
        }

        public async Task AddToFavorites(int userId, int recipeId, FavoriteType favoriteType)
        {
            var recipeExist = await _repositoryBaseFavorite.GetByIdAsync(recipeId);
            if (recipeExist == null)
            {
                throw new NotFoundException(HttpStatusCode.NotFound, "Receta no encontrada.");
            };
            var user = await _repositoryBaseUser.GetByIdAsync(userId);
            if (user == null)
            {
                throw new NotFoundException(HttpStatusCode.NotFound, "Usuario no encontrado.");
            };
            
            var response = await _repositoryBaseFavorite.AddAsync(recipeExist);


        }

        public Task<List<RecipeResponse>> GetFavoritesByUserAndType(int userId, FavoriteType favoriteType)
        {
            throw new NotImplementedException();
        }
    }
}
