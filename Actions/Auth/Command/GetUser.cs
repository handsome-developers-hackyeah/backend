using Comptee.DataAccess;
using Comptee.DataAccess.Entities;
using MediatR;

namespace Comptee.Actions.Auth.Command;

public static class GetUser
{
    public sealed record GetUserQuery(Guid UserId) : IRequest<User>;

    public class Handler : IRequestHandler<GetUserQuery, User>
    {
        public readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<User?> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Users.GetByIdAsync(request.UserId, cancellationToken);
        }
    }
}