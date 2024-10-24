using Application.Interfaces;
using Application.Mappings;
using Application.Models.Request;
using Application.Models.Response;
using Domain.Entities;
using Domain.Enum;
using Domain.Exceptions;
using Domain.Interfaces;
using System.Net;

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
                throw new NotFoundException(HttpStatusCode.NotFound, "Receta no encontrada.");
            };
            var entity = _commentMapping.FromRequestToEntity(request, userId);
            var response = await _repositoryBaseComment.AddAsync(entity);
            var user = await _repositoryBaseUser.GetByIdAsync(userId);

            if (user == null)
            {
                throw new NotFoundException(HttpStatusCode.NotFound, "Usuario no encontrado.");
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
                throw new NotFoundException(HttpStatusCode.NotFound, "Receta no encontrada.");
            }
            var comments = await _commentRepository.GetCommentsByRecipe(recipeId);
            var commentExist = comments.FirstOrDefault(c => c.Id == commentId);

            if (commentExist == null)

            {
                throw new NotFoundException(HttpStatusCode.NotFound, "Comentario no encontrado.");
            }
            if (role == Role.Common)
            {
                if (commentExist.UserId != userId)
                {
                    throw new NotAllowedException(HttpStatusCode.Forbidden, "Comentario no pertenece al usuario");
                }
            }
            await _repositoryBaseComment.DeleteAsync(commentExist);
        }
        public async Task<List<CommentResponse>> GetCommentsByRecipe(int recipeId)
        {
            var response = await _commentRepository.GetCommentsByRecipe(recipeId);
            var responseMapped = response.Select(c => _commentMapping.FromEntityToResponse(c)).ToList();
            return responseMapped;
        }
        public async Task ModifyComment(CommentRequest request, int commentId, int userId)
        {
            var recipeExist = await _repositoryBaseRecipe.GetByIdAsync(request.RecipeId);
            if (recipeExist == null)
            {
                throw new NotFoundException(HttpStatusCode.NotFound, "Receta no encontrada.");
            };
            var commentExist = await _repositoryBaseComment.GetByIdAsync(commentId);
            if (commentExist == null)
            {
                throw new NotFoundException(HttpStatusCode.NotFound, "Comentario no encontrado.");
            }
            if (commentExist.UserId != userId)
            {
                throw new NotAllowedException(HttpStatusCode.Forbidden, "Comentario no pertenece al usuario");
            }
            var entity = _commentMapping.FromEntityToEntityUpdated(request, commentExist);
            await _repositoryBaseComment.UpdateAsync(entity);
        }
    }
}
