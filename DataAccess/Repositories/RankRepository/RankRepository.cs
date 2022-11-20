using Comptee.DataAccess.Entities;
using Comptee.DataAccess.Repositories.BaseRepository;
using Microsoft.EntityFrameworkCore;

namespace Comptee.DataAccess.Repositories.RankRepository;

internal sealed class RanksRepository : BaseRepository<Ranks>, IRankRepository
{
    public RanksRepository(DbSet<Ranks>? entities) : base(entities)
    {
    }

    public Task<Ranks?> IsNextLevel(int? points, CancellationToken cancellationToken)
    {
        return _entities.FirstOrDefaultAsync(c => c.Points == points, cancellationToken);
    }

    public Task<Ranks?> GetPreviousLevel(int? points, CancellationToken cancellationToken)
    {
        return _entities.FirstOrDefaultAsync(c => c.Points < points, cancellationToken);
    }

    public Task<Ranks?> GetNextLevel(int? points, CancellationToken cancellationToken)
    {
        return _entities.FirstOrDefaultAsync(c => c.Points > points, cancellationToken);
    }
    

    public Task<Ranks?> GetDefaultRank(CancellationToken cancellationToken)
    {
        return _entities.OrderBy(c => c.Points).FirstAsync(cancellationToken);
    }
    
}