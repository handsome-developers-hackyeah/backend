using Comptee.DataAccess.Entities;
using Comptee.DataAccess.Repositories.BaseRepository;
using Comptee.DTO;
using Microsoft.EntityFrameworkCore;

namespace Comptee.DataAccess.Repositories.PostRepository;

internal sealed class PostRepository : BaseRepository<Post>, IPostRepository
{

    public PostRepository(DbSet<Post>? entities) : base(entities)
    {
    }

    public Task<List<PostDTO>>GetByCity(string city, int pageNumber, int pageSize, CancellationToken cancellationToken =default)
    {
        return _entities
            .AsQueryable()?
            //.Where(c => c.User.City == city)
            .OrderByDescending(c => c.Date)
            .Skip(pageNumber * pageSize)
            .Take(pageSize)
            .Select(c => new PostDTO
            {
                Amount = c.Amount,
                Id = c.Id,
                Localization = c.Localization,
                User = c.User,
                RespondCount = c.Responds.Count,
                Date = DateTime.Parse(c.Date),
                Comments = c.Comments,
                Responds = c.Responds,
                AlreadyFollow = false,
                UserId = c.UserId
            })
            .ToListAsync(cancellationToken);
    }

    public User? GetUser(Guid Id)
    {
        return _entities.FirstOrDefault(c => c.Id == c.Id).User;
    }

    // public Task<List<PostDTO>> GetByRegion(string region, int pageNumber, int pageSize, CancellationToken cancellationToken =default)
    // {
    //     return _entities.Where(c => c.User.Region == region)?.Select(c => new PostDTO
    //     {
    //         Amount = c.Amount,
    //         Date = c.Date,
    //         Id = c.Id,
    //         Localization = c.Localization,
    //         User = c.User,
    //     }).OrderBy(c => c.Date).Skip(pageNumber * pageSize).Take(pageSize).ToListAsync(cancellationToken);
    // }
}