using Comptee.DataAccess;
using Comptee.Exceptions;
using FluentValidation;
using MediatR;

namespace Comptee.Actions.BeComptee.Command;

public static class Unrespond
{
    public sealed record UnrespondCommand(Guid UserId, Guid PostId) : IRequest<Unit>;

    public class Handler : IRequestHandler<UnrespondCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UnrespondCommand request, CancellationToken cancellationToken)
        {
            var post = await _unitOfWork.Post.GetByIdAsync(request.PostId, cancellationToken);
            if (post is null)
            {
                throw new EntityNotFoundException("Post", request.PostId);
            }            
            var user = await _unitOfWork.Users.GetByIdAsync(request.UserId, cancellationToken);
            if (user is null)
            {
                throw new EntityNotFoundException("Users", request.UserId);
            }
            var existRespond = await _unitOfWork.Responds.GetByRelation(request.PostId, request.UserId, cancellationToken);
            if (existRespond is null)
            {
                throw new InvalidRequestException("Respond is not exist");
            }

            user.LikeSum -= 1;
            
            _unitOfWork.Users.Update(user);

            var nextRang = await _unitOfWork.Ranks.IsNextLevel(user.LikeSum, cancellationToken);
            if (nextRang is not null)
            {
                user.Rank = nextRang;
                user.Rank.Id = nextRang.Id;
            }
            
            _unitOfWork.Users.Update(user);
            
            Guid id = existRespond.Id;
            
            _unitOfWork.Responds.Remove(existRespond);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }

        public sealed class Validator : AbstractValidator<UnrespondCommand>
        {
            public Validator()
            {
                RuleFor(c => c.PostId).NotEqual(Guid.Empty);
                RuleFor(c => c.UserId).NotEqual(Guid.Empty);
            }
        }
    }
}