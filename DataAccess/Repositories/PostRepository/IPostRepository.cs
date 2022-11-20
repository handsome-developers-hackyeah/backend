using Comptee.DataAccess.Entities;
using Comptee.DataAccess.Repositories.BaseRepository;
using Comptee.DTO;

namespace Comptee.DataAccess.Repositories.RespondRepository;

public interface IPostRepository : IBaseRepository<Post>
{
    Task<List<PostDTO>> GetByCity(string city, int pageNumber, int pageSize, CancellationToken cancellationToken = default);
    User? GetUser(Guid Id);
    //Task<List<PostDTO>> GetByRegion(string region, int pageNumber, int pageSize, CancellationToken cancellationToken = default);
}
