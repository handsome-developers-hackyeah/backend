using Comptee.DataAccess.Enities.Abstract;
using Comptee.Enums;

namespace Comptee.DataAccess.Entities;

public class Respond : Entity
{
    public Guid UserId { get; set; }
    public User User { get; set; }    
    public Guid BeCompteeActivityId { get; set; }
    public BeCompteeActivity BeCompteeActivity { get; set; }
    public RespondType Type { get; set; }
}