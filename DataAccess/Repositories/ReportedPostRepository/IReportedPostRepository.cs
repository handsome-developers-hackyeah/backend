using Comptee.DataAccess.Entities;
using Comptee.DataAccess.Repositories.BaseRepository;
using Comptee.DTO;

namespace Comptee.DataAccess.Repositories.ReportedPostRepository;

public interface IReportedPostRepository : IBaseRepository<ReportedPosts>
{
    Task<List<ReportedPostDTO>> GetPosts(int pageNumber, int pageSize, CancellationToken cancellationToken);
    void RemoveByPostId(Guid id);
    Task<bool> ExitsPostByPostId(Guid id, CancellationToken cancellationToken);
}
