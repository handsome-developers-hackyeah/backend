using System.Data;
using Comptee.DataAccess;
using Comptee.DataAccess.Entities;
using Comptee.Exceptions;
using FluentValidation;
using MediatR;

namespace Comptee.Actions.Auth.Command;

public static class UpdateUser
{
    public sealed record UpdateUserCommand(User User) : IRequest<Unit>;

    public sealed class Handler : IRequestHandler<UpdateUserCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(request.User.Id, cancellationToken);

            if (user is null)
            {
                throw new EntityNotFoundException($"user with id {request.User.Id} not found");
            }
            user.Name = request.User.Name ?? user.Name;
            user.Email = request.User.Email ?? user.Name;
            

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }

    public sealed class Validator : AbstractValidator<UpdateUserCommand>
    {
        public Validator()
        {
            RuleFor(c => c.User.Id).NotEqual(Guid.Empty);
        }
    }
}