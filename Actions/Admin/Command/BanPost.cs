using Comptee.DataAccess;
using Comptee.DataAccess.Entities;
using Comptee.Exceptions;
using FluentValidation;
using MediatR;

namespace Comptee.Actions.Admin.Command;

public static class BanPost
{
    public sealed record BanPostCommand(Guid PostId) : IRequest<Unit>;

    public class Handler : IRequestHandler<BanPostCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(BanPostCommand request, CancellationToken cancellationToken)
        {
            if (!await _unitOfWork.ReportedPost.ExitsPostByPostId(request.PostId, cancellationToken))
            {
                throw new EntityNotFoundException("Post not found");
            }
            
            _unitOfWork.ReportedPost.RemoveByPostId(request.PostId);
            _unitOfWork.Post.RemoveById(request.PostId);
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }

        public sealed class Validator : AbstractValidator<BanPostCommand>
        {
            public Validator()
            {
                RuleFor(x => x.PostId).NotEqual(Guid.Empty);
            }
        }
    }
}