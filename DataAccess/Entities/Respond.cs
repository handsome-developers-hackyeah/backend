using Comptee.DataAccess.Enities.Abstract;
using Comptee.Enums;

namespace Comptee.DataAccess.Entities;

public class Respond : Entity
{
    public Guid UserId { get; set; }
    public User User { get; set; }    
    public Guid PostId { get; set; }
    public Post Post { get; set; }
    public RespondType Type { get; set; }
}