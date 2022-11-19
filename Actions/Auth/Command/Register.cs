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
    public sealed record Command
        (string Name, string Email, string Password, bool HaveAvatar, string Base64) : IRequest<Unit>;

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly string _avatarPath;

        public Handler(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _avatarPath = configuration["AvatarPath"]!;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetByEmail(request.Email, cancellationToken);
            if (user is not null)
            {
                throw new InvalidRequestException($"user with email {request.Email} already exists");
            }

            user = new User
            {
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Name = request.Name,
                HaveAvatar = request.HaveAvatar,
                IsActive = false,
                Role = JwtPolicies.User,
                Id = Guid.NewGuid()
            };

            if (request.HaveAvatar)
            {
                await File.WriteAllBytesAsync($"{_avatarPath}/{user.Id}.txt",
                    Encoding.UTF8.GetBytes(request.Base64), cancellationToken);
            }

            await _unitOfWork.Users.AddAsync(user, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        public sealed class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(c => c.Name).MinimumLength(3);
            }
        }
    }
}