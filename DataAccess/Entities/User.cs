using Comptee.DataAccess.Enities.Abstract;

namespace Comptee.DataAccess.Entities;

public class User : Entity
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? PasswordHash { get; set; }
    public bool? HaveAvatar { get; set; }
    public bool? IsActive { get; set; }
    public string? Role { get; set; }
    public string? City { get; set; }
    public string? Region { get; set; }
    public int? Rank { get; set; }
    public int? PlotSize { get; set; }
    public int? NumberOfResidents { get; set; }
    public int? BanedPost { get; set; }
    public bool? IsBan { get; set; }
    public ICollection<Respond>? Responds { get; set; }
    public ICollection<Post>? Posts { get; set; }
    public ICollection<ReportedPosts>? ReportedPosts { get; set; }
    public ICollection<Comment>? Comments { get; set; }


}