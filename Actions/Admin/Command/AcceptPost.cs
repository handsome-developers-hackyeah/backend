using Comptee.DataAccess;
using Comptee.Exceptions;
using MediatR;

namespace Comptee.Actions.Admin.Command;

public static class AcceptPost
{
    public sealed record AcceptPostCommand(Guid PostId) : IRequest<Unit>;

    public class Handler : IRequestHandler<AcceptPostCommand, Unit>
    {
        public readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(AcceptPostCommand request, CancellationToken cancellationToken)
        {
            if (!await _unitOfWork.ReportedPost.ExitsPostByPostId(request.PostId, cancellationToken))
            {
                throw new EntityNotFoundException("Post not found");
            }
            
            _unitOfWork.ReportedPost.RemoveByPostId(request.PostId);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}