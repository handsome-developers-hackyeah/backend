using Comptee.DataAccess.Entities;
using Comptee.DataAccess.Repositories.BaseRepository;
using Comptee.DataAccess.Repositories.PostRepository;
using Comptee.DataAccess.Repositories.RespondRepository;
using Comptee.DataAccess.Repositories.UserRepository;
using Microsoft.EntityFrameworkCore;

namespace Comptee.DataAccess;

public class DataContext : DbContext, IUnitOfWork
{
    private DbSet<User>? _Users { get; set; }
    public IUserRepository Users => new UserRepository(_Users);
    
    
    private DbSet<Post>? _Post { get; set; }
    public IPostRepository Post => new PostRepository(_Post);    
    
    private DbSet<Respond>? _Responds { get; set; }
    public IRespondRepository Responds => new RespondRepository(_Responds); 
    
    private DbSet<ReportedPosts>? _ReportedPost { get; set; }
    public IBaseRepository<ReportedPosts> ReportedPost => new BaseRepository<ReportedPosts>(_ReportedPost);    
    
    private DbSet<Comment>? _Comment { get; set; }
    public IBaseRepository<Comment> Comment => new BaseRepository<Comment>(_Comment);

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
}