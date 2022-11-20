using System.Text;
using Comptee.DataAccess;
using Comptee.DataAccess.Entities;
using Comptee.Exceptions;
using Comptee.Jwt;
using FluentValidation;
using MediatR;

namespace Comptee.Actions.Auth.Command;

public static class Register
{
    public sealed record Command(string Name, string Email, string Password) : IRequest<GeneratedToken>;

    public class Handler : IRequestHandler<Command, GeneratedToken>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtAuth _jwtAuth;

        public Handler(IUnitOfWork unitOfWork, IConfiguration configuration, IJwtAuth jwtAuth)
        {
            _unitOfWork = unitOfWork;
            _jwtAuth = jwtAuth;
        }

        public async Task<GeneratedToken> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetByEmail(request.Email, cancellationToken);
            if (user is not null)
            {
                throw new InvalidRequestException($"user with email {request.Email} already exists");
            }

            Ranks? defaultRank = await _unitOfWork.Ranks.GetDefaultRank(cancellationToken);
            
            user = new User
            {
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Name = request.Name,
                HaveAvatar = false,
                IsActive = false,
                Role = JwtPolicies.User,
                Id = Guid.NewGuid(),
                City = "",
                Rank = defaultRank,
                IsBan = false,
                BanedPost = 0,
                Region = "",
                PlotSize = 0,
                NumberOfResidents = 0,
                LikeSum = 0
            };

            await _unitOfWork.Users.AddAsync(user, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return await _jwtAuth.GenerateJwt(user);
        }

        public sealed class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(c => c.Name).MinimumLength(3);
                RuleFor(c => c.Email).EmailAddress();
                RuleFor(c => c.Password).MinimumLength(8);
            }
        }
    }
}