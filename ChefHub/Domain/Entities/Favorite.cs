namespace Domain.Entities
{
    public class Favorite
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RecipeId { get; set; }
        public User? User { get; set; }
        public Recipe? Recipe { get; set; }
    }
}
