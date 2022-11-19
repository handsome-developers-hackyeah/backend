using Comptee.DataAccess;
using Comptee.Exceptions;
using Comptee.Jwt;
using MediatR;

namespace Comptee.Actions.Auth.Query;

public static class RefreshToken
{
    public sealed record RefreshTokenQuery(Guid Id) : IRequest<GeneratedToken>;

    public class Handler : IRequestHandler<RefreshTokenQuery, GeneratedToken>
    {
        public readonly IUnitOfWork _unitOfWork;
        public readonly IJwtAuth _jwtAuth;

        public Handler(IUnitOfWork unitOfWork, IConfiguration configuration, IJwtAuth jwtAuth)
        {
            _unitOfWork = unitOfWork;
            _jwtAuth = jwtAuth;
        }

        public async Task<GeneratedToken> Handle(RefreshTokenQuery request, CancellationToken cancellationToken)
        {
            var user =await _unitOfWork.Users.GetByIdAsync(request.Id, cancellationToken);
            if (user is null)
            {
                throw new EntityNotFoundException("User not Found");
            }
            return await _jwtAuth.GenerateJwt(user);
            
        }
    }
}