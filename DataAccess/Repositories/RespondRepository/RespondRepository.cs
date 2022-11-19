using Comptee.DataAccess.Entities;
using Comptee.DataAccess.Repositories.BaseRepository;
using Microsoft.EntityFrameworkCore;

namespace Comptee.DataAccess.Repositories.PostRepository;

internal sealed class RespondRepository : BaseRepository<Respond>, IRespondRepository
{
    public RespondRepository(DbSet<Respond>? entities) : base(entities)
    {
    }

    public Task<Respond?> GetByRelation(Guid postId, Guid userId, CancellationToken cancellationToken = default)
    {
        return _entities.FirstOrDefaultAsync(c => c.UserId == userId && c.PostId == postId, cancellationToken);
    }
}