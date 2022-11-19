using Comptee.DataAccess.Entities;
using Comptee.DataAccess.Repositories.BaseRepository;

namespace Comptee.DataAccess.Repositories.PostRepository;

public interface IRespondRepository : IBaseRepository<Respond>
{
    Task<Respond?> GetByRelation(Guid postId, Guid userId, CancellationToken cancellationToken = default);
}
