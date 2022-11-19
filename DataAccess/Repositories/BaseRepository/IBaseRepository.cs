using Comptee.DataAccess.Enities.Abstract;

namespace Comptee.DataAccess.Repositories.BaseRepository;

public interface IBaseRepository<TEntity> where TEntity : Entity
{
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    void Update(TEntity entity);
    ValueTask<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    IQueryable<TEntity> GetQueryable();
    Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    void RemoveById(Guid id);
    void Remove(TEntity entity);
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
}