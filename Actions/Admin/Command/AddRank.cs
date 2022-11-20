using Comptee.DataAccess;
using Comptee.DataAccess.Entities;
using FluentValidation;
using MediatR;

namespace Comptee.Actions.Admin.Command;

public static class AddRank
{
    public sealed record AddRanksCommand(string Name, int Points) : IRequest<Unit>;

    public class Handler : IRequestHandler<AddRanksCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(AddRanksCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.Ranks.AddAsync(new Ranks()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Points = request.Points
            }, cancellationToken);
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }

        public sealed class Validator : AbstractValidator<AddRanksCommand>
        {
            public Validator()
            {

            }
        }
    }
}