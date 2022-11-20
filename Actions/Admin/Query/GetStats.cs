using Comptee.DataAccess;
using Comptee.DTO;
using MediatR;

namespace Comptee.Actions.Admin.Query;

public static class GetStats
{
    public sealed record Query() : IRequest<StatsObject>;

    public class Handler : IRequestHandler<Query, StatsObject>
    {
        public readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<StatsObject> Handle(Query request, CancellationToken cancellationToken)
        {
            var data = await _unitOfWork.Post.GetAllAsync(cancellationToken);
            int sumOfComptee = data.Sum(c => c.Amount);
            
            int usersCount = (await _unitOfWork.Users.GetAllAsync(cancellationToken)).Count;
            int postCount = (await _unitOfWork.Post.GetAllAsync(cancellationToken)).Count;
            
            return new StatsObject()
            {
                CompostSum = sumOfComptee,
                UsersSum = usersCount,
                PostCount = postCount,
                CompostAverage = sumOfComptee+1 / usersCount+1,
            };
        }
    }
}