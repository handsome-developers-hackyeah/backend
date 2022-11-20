using Comptee.DataAccess;
using Comptee.DataAccess.Entities;
using Comptee.DTO;
using Comptee.Exceptions;
using MediatR;

namespace Comptee.Actions.Auth.Query;

public static class GetUser
{
    public sealed record GetUserQuery(Guid UserId) : IRequest<UserWithLevels>;

    public class Handler : IRequestHandler<GetUserQuery, UserWithLevels>
    {
        public readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UserWithLevels?> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(request.UserId, cancellationToken);
            if (user is null)
            {
                throw new EntityNotFoundException("User Not Found");
            }

            UserWithLevels result = new UserWithLevels()
            {
                User = user,
                NextRank = await _unitOfWork.Ranks.GetNextLevel(user.LikeSum, cancellationToken),
                PreviousRank = await _unitOfWork.Ranks.GetPreviousLevel(user.LikeSum, cancellationToken),
                CurrentRank = await _unitOfWork.Ranks.GetByIdAsync(user.RankId, cancellationToken)
            };
            result.CurrentRank!.Users = null;
            result.PointsToNextRank = (int) (result.NextRank!.Points - result.User.LikeSum);
            return result;
        }
    }
}