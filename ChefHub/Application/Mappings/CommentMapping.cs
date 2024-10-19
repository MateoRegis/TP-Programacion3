using Application.Models.Request;
using Application.Models.Response;
using Domain.Entities;

namespace Application.Mappings
{
    public class CommentMapping
    {
        public Comment FromRequestToEntity(CommentRequest request, int userId)
        {
            return new Comment
            {
                Score = request.Score,
                Text = request.Text,
                UserId = userId,
                RecipeId = request.RecipeId,
            };
        }
        public CommentResponse FromEntityToResponse(Comment entity)
        {
            var mapping = new UserMapping();
            return new CommentResponse
            {
                Id = entity.Id,
                Score = entity.Score,
                Text = entity.Text,
                UserResponse = mapping.FromUserToResponse(entity.User)
            };
        }

        public Comment FromEntityToEntityUpdated(CommentRequest request, Comment comment)
        {
            comment.Score = request.Score;
            comment.Text = request.Text ?? comment.Text;
            return comment;
        }
    }
}
