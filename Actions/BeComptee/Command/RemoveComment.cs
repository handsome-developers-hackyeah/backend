using Comptee.DataAccess;
using Comptee.Exceptions;
using FluentValidation;
using MediatR;

namespace Comptee.Actions.BeComptee.Command;

public static class RemoveComment
{
    public sealed record RemoveCommentCommand(Guid CommentId) : IRequest<Unit>;

    public class Handler : IRequestHandler<RemoveCommentCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(RemoveCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = await _unitOfWork.Comment.GetByIdAsync(request.CommentId, cancellationToken);
            if (comment is null)
            {
                throw new EntityNotFoundException("Comment Not Found");
            }
            
            _unitOfWork.Comment.Remove(comment);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }

        public sealed class Validator : AbstractValidator<RemoveCommentCommand>
        {
            public Validator()
            {
                RuleFor(c => c.CommentId).NotEqual(Guid.Empty);
            }
        }
    }
}