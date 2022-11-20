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
            return new StatsObject()
            {
                
            };
        }
    }
}