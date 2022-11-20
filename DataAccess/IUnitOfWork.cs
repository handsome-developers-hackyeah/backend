using Comptee.DataAccess.Entities;
using Comptee.DataAccess.Repositories.BaseRepository;
using Comptee.DataAccess.Repositories.PostRepository;
using Comptee.DataAccess.Repositories.RankRepository;
using Comptee.DataAccess.Repositories.ReportedPostRepository;
using Comptee.DataAccess.Repositories.UserRepository;

namespace Comptee.DataAccess;

public interface IUnitOfWork
{
    IUserRepository Users { get; }
    IRespondRepository Responds { get; }
    IPostRepository Post { get; }
    IReportedPostRepository ReportedPost { get; }
    public IBaseRepository<Comment> Comment { get; }
    public IRankRepository Ranks { get; }
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}