using Comptee.DataAccess;
using Comptee.Exceptions;
using FluentValidation;
using MediatR;

namespace Comptee.Actions.Auth.Command;

public static class ChangePhoto
{
    public sealed record ChangePhotoCommand(Guid Id, string Base64) : IRequest<Unit>;

    public class Handler : IRequestHandler<ChangePhotoCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly string? _pathToAvatars;

        public Handler(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _pathToAvatars = configuration["PathToAvatars"];
        }

        public async Task<Unit> Handle(ChangePhotoCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(request.Id, cancellationToken);
            if (user is null)
            {
                throw new EntityNotFoundException("User not exist");
            }

            user.HaveAvatar = true;
            try
            {
                await File.WriteAllTextAsync(_pathToAvatars + @"/" + request.Id + ".txt", request.Base64, cancellationToken);
            }
            catch (Exception e)
            {
                await File.WriteAllTextAsync($@"/{request.Id}", request.Base64, cancellationToken);
            }

            _unitOfWork.Users.Update(user);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }

        public sealed class Validator : AbstractValidator<ChangePhotoCommand>
        {
            public Validator()
            {
                RuleFor(c => c.Id).NotEqual(Guid.Empty);
            }
        }
    }
}