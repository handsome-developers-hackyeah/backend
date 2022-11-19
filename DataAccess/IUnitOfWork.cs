using Comptee.DataAccess.Repositories.BaseRepository;
using Comptee.DataAccess.Repositories.UserRepository;

namespace Comptee.DataAccess;

public interface IUnitOfWork
{
    IUserRepository Users { get; }
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}