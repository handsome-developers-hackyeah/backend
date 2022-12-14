using Comptee.DataAccess.Enities.Abstract;

namespace Comptee.DataAccess.Entities;

public class Comment : Entity
{
    public Guid UserId { get; set; }
    public User User { get; set; }    
    public Guid PostId { get; set; }
    public Post Post { get; set; }
    public string Content { get; set; }
}