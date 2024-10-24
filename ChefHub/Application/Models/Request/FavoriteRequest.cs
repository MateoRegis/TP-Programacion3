using Domain.Enum;

namespace Application.Models.Request
{
    public class FavoriteRequest
    {
        public int? RecipeId { get; set; }
        public FavoriteType? FavoriteType { get; set; }
    }
}
