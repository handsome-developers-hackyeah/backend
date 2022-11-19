using Comptee.DataAccess.Enities.Abstract;

namespace Comptee.DataAccess.Entities;

public class BeCompteeActivity : Entity
{
    public Guid UserId { get; set; }
    public User? User { get; set; }
    public string Localization { get; set; }
    public ICollection<Respond>? Responds { get; set; }
    public int Amount { get; set; }
    public DateTime? Date { get; set; }
}