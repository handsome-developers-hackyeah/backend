using Comptee.DataAccess;
using Comptee.Exceptions;
using FluentValidation;
using MediatR;

namespace Comptee.Actions.BeComptee.Command;

public static class AddComment
{
    public sealed record AddCommentCommand(Guid UserId, Guid PostId, string Content) : IRequest<Unit>;

    public class Handler : IRequestHandler<AddCommentCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(AddCommentCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(request.UserId, cancellationToken);
            if (user is null)
            {
                throw new EntityNotFoundException("User Not Found");
            }            
            var post = await _unitOfWork.Post.GetByIdAsync(request.PostId, cancellationToken);
            if (post is null)
            {
                throw new EntityNotFoundException("Post Not Found");
            }

            await _unitOfWork.Comment.AddAsync(new DataAccess.Entities.Comment()
            {
                Content = request.Content,
                PostId = request.PostId,
                UserId = request.UserId
            }, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }

        public sealed class Validator : AbstractValidator<AddCommentCommand>
        {
            public Validator()
            {
                RuleFor(c => c.PostId).NotEqual(Guid.Empty);
                RuleFor(c => c.UserId).NotEqual(Guid.Empty);
                RuleFor(c => c.Content).MaximumLength(100);

            }
        }
    }
}