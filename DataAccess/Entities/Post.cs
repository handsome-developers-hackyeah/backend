using Comptee.DataAccess.Enities.Abstract;

namespace Comptee.DataAccess.Entities;

public class Post : Entity
{
    public Guid UserId { get; set; }
    public User? User { get; set; }
    public string Localization { get; set; }
    public ICollection<Respond>? Responds { get; set; }
    public ICollection<ReportedPosts>? ReportedPosts { get; set; }
    public ICollection<Comment>? Comments { get; set; }
    public int Amount { get; set; }
    public int ReportCount { get; set; }
    public string? Date { get; set; }
    
}