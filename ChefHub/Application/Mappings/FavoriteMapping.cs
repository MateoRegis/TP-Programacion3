using Application.Models.Request;
using Domain.Entities;

namespace Application.Mappings
{
    public class FavoriteMapping
    {
        public Favorite FromRequestToEntity(int userId, FavoriteRequest favoriteRequest)
        {
            var entity = new Favorite
            {
                UserId = userId,
                RecipeId = favoriteRequest.RecipeId,
                FavoriteType = favoriteRequest.FavoriteType
            };
            return entity;
        }
    }
}