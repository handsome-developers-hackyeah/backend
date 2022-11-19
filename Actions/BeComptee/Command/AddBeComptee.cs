using Comptee.DataAccess;
using Comptee.DataAccess.Entities;
using Comptee.Exceptions;
using FluentValidation;
using MediatR;

namespace Comptee.Actions.BeComptee.Command;

public static class AddBeComptee
{
    public sealed record AddBeCompteeCommand(BeCompteeActivity Activity, string Photo) : IRequest<Unit>;

    public class Handler : IRequestHandler<AddBeCompteeCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(AddBeCompteeCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(request.Activity.UserId, cancellationToken);
            if (user is null)
            {
                throw new EntityNotFoundException("User not found");
            }
            
            request.Activity.Date = DateTime.Now;

            await _unitOfWork.
            
            return Unit.Value;
        }

        public sealed class Validator : AbstractValidator<AddBeCompteeCommand>
        {
            public Validator()
            {

            }
        }
    }
}