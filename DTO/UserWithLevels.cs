using Comptee.DataAccess.Entities;

namespace Comptee.DTO;

public class UserWithLevels
{
    public User User { get; set; }    
    public Ranks? PreviousRank { get; set; }
    public Ranks? NextRank { get; set; }
    public Ranks? CurrentRank { get; set; }
    public int PointsToNextRank { get; set; }
}
