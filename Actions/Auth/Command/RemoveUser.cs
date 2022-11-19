using Comptee.DataAccess;
using Comptee.Exceptions;
using FluentValidation;
using MediatR;

namespace Comptee.Actions.Auth.Command;


public static class RemoveUser
{
    public sealed record RemoveUserCommand(Guid Id) : IRequest<Unit>;

    public class Handler : IRequestHandler<RemoveUserCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
        {
            if (!await _unitOfWork.Users.ExistsAsync(request.Id, cancellationToken))
            {
                throw new EntityNotFoundException($"user with id {request.Id} not found");
            }

            _unitOfWork.Users.RemoveById(request.Id);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
    public sealed class Validator : AbstractValidator<RemoveUserCommand>
    {
        public Validator()
        {
                
        }
    }
}