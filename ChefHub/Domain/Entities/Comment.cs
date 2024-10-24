namespace Domain.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string? Text { get; set; }
        public int? Score { get; set; }
        public int UserId { get; set; }
        public int? RecipeId { get; set; }
        public User? User { get; set; }
        public Recipe? Recipe { get; set; }
    }
}
