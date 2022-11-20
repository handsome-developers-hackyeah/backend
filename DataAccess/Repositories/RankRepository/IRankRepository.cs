using Comptee.DataAccess.Entities;
using Comptee.DataAccess.Repositories.BaseRepository;

namespace Comptee.DataAccess.Repositories.RankRepository;

public interface IRankRepository : IBaseRepository<Ranks>
{
    Task<Ranks?> IsNextLevel(int? points, CancellationToken cancellationToken);
    Task<Ranks?> GetPreviousLevel(int? points, CancellationToken cancellationToken);
    Task<Ranks?> GetNextLevel(int? points, CancellationToken cancellationToken);
    Task<Ranks?> GetDefaultRank(CancellationToken cancellationToken);
}
