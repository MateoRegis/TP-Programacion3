using Domain.Enum;

namespace Application.Models.Request
{
    public class FavoriteRequest
    {
        public int UserId { get; set; }
        public int RecipeId { get; set; }
        public FavoriteType FavoriteType { get; set; }
    }
}
