using Comptee.DataAccess.Entities;
using Comptee.DataAccess.Repositories.BaseRepository;
using Comptee.DTO;
using Microsoft.EntityFrameworkCore;

namespace Comptee.DataAccess.Repositories.ReportedPostRepository;

internal sealed class ReportedPostRepository : BaseRepository<ReportedPosts>, IReportedPostRepository
{
    private IReportedPostRepository _reportedPostRepositoryImplementation;

    public ReportedPostRepository(DbSet<ReportedPosts>? entities) : base(entities)
    {
    }


    public Task<List<ReportedPostDTO>> GetPosts(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        return _entities.Select(c => new ReportedPostDTO()
        {
            Post = c.Post,
            User = c.Post.User,
        }).Skip(pageNumber * pageSize).Take(pageSize).ToListAsync(cancellationToken);
    }

    public void RemoveByPostId(Guid id)
    {
        var post = _entities.FirstOrDefault(c => c.PostId == id);
        Remove(post);
    }

    public Task<bool> ExitsPostByPostId(Guid id, CancellationToken cancellationToken)
    {
        return _entities.AnyAsync(c => c.PostId == id, cancellationToken);
    }
}