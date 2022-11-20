using Comptee.DataAccess;
using Comptee.DataAccess.Entities;
using Comptee.DTO;
using MediatR;

namespace Comptee.Actions.Admin.Query;

public static class GetReportedPosts
{
    public sealed record Query(int pageNumber) : IRequest<List<ReportedPostDTO>>;

    public class Handler : IRequestHandler<Query, List<ReportedPostDTO>>
    {
        public readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ReportedPostDTO>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.ReportedPost.GetPosts(request.pageNumber, 10, cancellationToken);
        }
    }
}