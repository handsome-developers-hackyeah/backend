using Comptee.DataAccess.Entities;
using Comptee.DataAccess.Repositories.UserRepository;
using Microsoft.EntityFrameworkCore;

namespace Comptee.DataAccess;

public class DataContext : DbContext, IUnitOfWork
{
    private DbSet<User>? _Users { get; set; }
    public IUserRepository Users => new UserRepository(_Users);

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
}