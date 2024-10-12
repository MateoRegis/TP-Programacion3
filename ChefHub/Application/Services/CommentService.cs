using Application.Interfaces;
using Application.Mappings;
using Application.Models.Request;
using Application.Models.Response;
using Domain.Entities;
using Domain.Enum;
using Domain.Interfaces;

namespace Application.Services
{
    public class CommentService : ICommentService
    {
        private readonly IRepositoryBase<Comment> _repositoryBaseComment;
        private readonly CommentMapping _commentMapping;
        private readonly ICommentRepository _commentRepository;
        private readonly IRepositoryBase<User> _repositoryBaseUser;
        private readonly IRepositoryBase<Recipe> _repositoryBaseRecipe;


        public CommentService(IRepositoryBase<Comment> repositoryBase, CommentMapping commentMapping, ICommentRepository commentRepository, IRepositoryBase<User> repositoryBaseUser, IRepositoryBase<Recipe> repositoryBaseRecipe)
        {
            _repositoryBaseComment = repositoryBase;
            _commentMapping = commentMapping;
            _commentRepository = commentRepository;
            _repositoryBaseUser = repositoryBaseUser;
            _repositoryBaseRecipe = repositoryBaseRecipe;
        }

        public async Task<CommentResponse> CreateComment(CommentRequest request, int userId)
        {
            var recipeExist = await _repositoryBaseRecipe.GetByIdAsync(request.RecipeId);
            if (recipeExist == null)
            {
                throw new Exception("-- No existe la recete a la que se le quiere agregar el Comentario--");
            };
            var entity = _commentMapping.FromRequestToEntity(request, userId);
            var response = await _repositoryBaseComment.AddAsync(entity);
            var user = await _repositoryBaseUser.GetByIdAsync(userId);

            if (user == null)
            {
                throw new Exception("-Usuario No Encontrado-");
            };
            response.User = user;

            var responseMapped = _commentMapping.FromEntityToResponse(response);
            return responseMapped;
        }

        public async Task DeleteComment(int recipeId, int commentId, int userId, Role role)
        {
            var recipeExist = await _repositoryBaseRecipe.GetByIdAsync(recipeId);

            if (recipeExist == null)
            {
                throw new Exception("receta no encontrada");
            }
            var comments = await _commentRepository.GetCommentsByRecipe(recipeId);
            var commentExist = comments.FirstOrDefault(c => c.Id == commentId);

            if (commentExist == null)

            {
                throw new Exception("comentario no encontrada");
            }
            if (role != Role.Moderator)
            {
                if (commentExist.UserId != userId)
                {
                    throw new Exception("coemntario no pertenece al usuario");

                }
            }
            await _repositoryBaseComment.DeleteAsync(commentExist);
        }

        public async Task<List<CommentResponse>> GetCommentsByRecipe(int recipeId)
        {
            var response = await _commentRepository.GetCommentsByRecipe(recipeId);
            var responSeMapped = response.Select(c => _commentMapping.FromEntityToResponse(c)).ToList();
            return responSeMapped;
        }



    }
}
