using Comptee.DataAccess;
using Comptee.DataAccess.Entities;
using Comptee.Exceptions;
using FluentValidation;
using MediatR;

namespace Handsomedevelopers.Actions;

public static class Report
{
    public sealed record ReportCommand(Guid PostId, Guid ReporterId) : IRequest<Unit>;

    public class Handler : IRequestHandler<ReportCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(ReportCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(request.ReporterId, cancellationToken);
            if (user is null)
            {
                throw new EntityNotFoundException("User Not Found");
            }            
            var post = await _unitOfWork.Post.GetByIdAsync(request.PostId, cancellationToken);
            if (post is null)
            {
                throw new EntityNotFoundException("Post Not Found");
            }

            await _unitOfWork.ReportedPost.AddAsync(new ReportedPosts()
            {
                PostId = request.PostId,
                ReporterId = request.ReporterId,
                ByReport = true
            }, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }

        public sealed class Validator : AbstractValidator<ReportCommand>
        {
            public Validator()
            {
                RuleFor(c => c.PostId).NotEqual(Guid.Empty);
                RuleFor(c => c.ReporterId).NotEqual(Guid.Empty);
            }
        }
    }
}