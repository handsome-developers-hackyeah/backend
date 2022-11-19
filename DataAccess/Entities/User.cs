using Comptee.DataAccess.Enities.Abstract;

namespace Comptee.DataAccess.Entities;

public class User : Entity
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? PasswordHash { get; set; }
    public bool? HaveAvatar { get; set; }
    public bool? IsActive { get; set; }
    public string? Role { get; set; }
}