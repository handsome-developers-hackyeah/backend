using Comptee.DataAccess.Enities.Abstract;

namespace Comptee.DataAccess.Entities;

public class ReportedPosts : Entity
{
    public Guid PostId { get; set; }
    public Post Post { get; set; }
    public Guid ReporterId { get; set; }
    public User Reporter { get; set; }
    public bool ByReport { get; set; }
}