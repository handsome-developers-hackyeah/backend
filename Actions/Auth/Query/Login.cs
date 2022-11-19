using Comptee.DataAccess;
using Comptee.Exceptions;
using Comptee.Jwt;
using MediatR;

namespace Comptee.Actions.Auth.Query;

public static class Login
{
    public sealed record Query(string Email, string Password) : IRequest<GeneratedToken>;

    public class Handler : IRequestHandler<Query, GeneratedToken>
    {
        private readonly string _avatarPath;
        public readonly IUnitOfWork _unitOfWork;
        private readonly IJwtAuth _jwtAuth;

        public Handler(IUnitOfWork unitOfWork, IJwtAuth jwtAuth, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _jwtAuth = jwtAuth;
            _avatarPath = configuration.GetValue<string>("AvatarPath")!;
        }

        public async Task<GeneratedToken> Handle(Query request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetByEmail(request.Email, cancellationToken);
            if (user is null)
            {
                throw new EntityNotFoundException("User not found");
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                throw new InvalidRequestException($"Bad password");
            }

            return await _jwtAuth.GenerateJwt(user);
        }
    }
}