using Comptee.DataAccess.Entities;
using Comptee.DataAccess.Repositories.BaseRepository;
using Microsoft.EntityFrameworkCore;

namespace Comptee.DataAccess.Repositories.UserRepository;

internal sealed class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(DbSet<User>? entities) : base(entities)
    {
    }

    public Task<User?> GetByEmail(string email, CancellationToken cancellationToken = default)
    {
        return _entities.FirstOrDefaultAsync(c => c.Email == email, cancellationToken);
    }
}