using Comptee.DataAccess.Entities;
using Comptee.DataAccess.Repositories.BaseRepository;

namespace Comptee.DataAccess.Repositories.UserRepository;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> GetByEmail(string email, CancellationToken cancellationToken);
}
