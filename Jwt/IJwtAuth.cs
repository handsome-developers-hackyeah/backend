using Comptee.DataAccess.Entities;

namespace Comptee.Jwt;

public interface IJwtAuth
{
    public Task<GeneratedToken> GenerateJwt(User user);
}
