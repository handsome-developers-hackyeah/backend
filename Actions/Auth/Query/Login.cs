using Comptee.DataAccess;
using Comptee.Exceptions;
using Comptee.Jwt;
using MediatR;

namespace Comptee.Actions.Auth.Query;

public static class Login
{
    public sealed record LoginQuery(string Email, string Password) : IRequest<GeneratedToken>;

    public class Handler : IRequestHandler<LoginQuery, GeneratedToken>
    {
        public readonly IUnitOfWork _unitOfWork;
        private readonly IJwtAuth _jwtAuth;

        public Handler(IUnitOfWork unitOfWork, IJwtAuth jwtAuth, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _jwtAuth = jwtAuth;
        }

        public async Task<GeneratedToken> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetByEmail(request.Email, cancellationToken);
            if (user is null)
            {
                throw new EntityNotFoundException("User not found");
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                throw new BadPassword($"Bad password");
            }

            return await _jwtAuth.GenerateJwt(user);
        }
    }
}