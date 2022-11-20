using Comptee.DataAccess.Entities;

namespace Comptee.DTO;

public class ReportedPostDTO
{
    public Post Post { get; set; }
    public User User { get; set; }
}