using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Comptee.DataAccess.Entities;
using Microsoft.IdentityModel.Tokens;

namespace Comptee.Jwt;

public class JwtAuth : IJwtAuth
{
    private readonly IConfiguration _configuration;

    public JwtAuth(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Task<GeneratedToken> GenerateJwt(User user)
    {
        byte[] key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]!);

        //tu add claims

        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("id", user.Id.ToString()),
                new Claim("email", user.Email),
                new Claim("name", user.Name),
                new Claim("haveAvatar", user.HaveAvatar.ToString()),
                new Claim(ClaimTypes.Role, JwtPolicies.User)
            }),
            Expires = DateTime.UtcNow.AddDays(int.Parse(_configuration["Jwt:ExpireDays"])),
            Audience = _configuration["Jwt:Audience"]!,
            Issuer = _configuration["Jwt:Issuer"]!,
            SigningCredentials = new SigningCredentials
            (new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature)
        };
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken? token = tokenHandler.CreateToken(tokenDescriptor);
        return Task.FromResult(new GeneratedToken(tokenHandler.WriteToken(token)));
    }
}