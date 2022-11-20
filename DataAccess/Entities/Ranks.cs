using Comptee.DataAccess.Enities.Abstract;

namespace Comptee.DataAccess.Entities;

public class Ranks : Entity
{
    public int Points { get; set; }
    public int Number { get; set; }
    public string? Name { get; set; }
    public ICollection<User>? Users { get; set; }
}