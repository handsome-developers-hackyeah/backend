using Comptee.DataAccess.Entities;

namespace Comptee.DTO;

public class PostDTO
{
    public Guid UserId { get; set; }
    public Guid Id { get; set; }
    public User? User { get; set; }
    public string Localization { get; set; }
    public int Amount { get; set; }
    public DateTime? Date { get; set; }
    public int RespondCount { get; set; }
    public bool AlreadyFollow { get; set; }
    public ICollection<Comment>? Comments { get; set; }
    public ICollection<Respond>? Responds { get; set; }
}