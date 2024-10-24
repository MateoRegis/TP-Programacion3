using Application.Models.Request;
using Application.Models.Response;
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
        public FavoriteResponse FromEntityToResponse(Favorite entity)
        {
            var mapping = new RecipeMapping();
            return new FavoriteResponse
            {
                Id = entity.Id,
                RecipeResponse = mapping.FromEntityToResponse(entity.Recipe)
            };
        }
        public Favorite FromEntityToEntityUpdated(FavoriteRequest request, Favorite entity)
        {
            entity.RecipeId = request.RecipeId ?? entity.RecipeId;
            entity.FavoriteType = request.FavoriteType ?? entity.FavoriteType;
            return entity;
        }
    }
}