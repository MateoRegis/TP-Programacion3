using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class CommentRepository : EFRepository<Comment>, ICommentRepository
    {
        public CommentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<Comment>> GetCommentsByRecipe(int recipeId)
        {
            return await _context.Comments.Include(c => c.User).Where(c => c.RecipeId == recipeId).ToListAsync();
        }
        //public async Task<Comment> CreateComment(Comment comment)
        //{
        //    await _context.Comments.AddAsync(comment);
        //    await _context.SaveChangesAsync();
        //    return comment;
        //}



    }
}
