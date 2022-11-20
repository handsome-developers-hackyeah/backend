using Comptee.DataAccess;
using Comptee.DTO;
using Comptee.Exceptions;
using Comptee.Jwt;
using MediatR;

namespace Comptee.Actions.BeComptee.Query;

public static class GetPost
{
    public sealed record GetPostQuery(Guid UserId, int Page) :  IRequest<List<PostDTO>>;

    public class Handler : IRequestHandler<GetPostQuery, List<PostDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly int _pageSize;


        public Handler(IUnitOfWork unitOfWork, IJwtAuth jwtAuth, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            
        }

        public async Task<List<PostDTO>> Handle(GetPostQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(request.UserId, cancellationToken);

            if (user is null)
            {
                throw new EntityNotFoundException("User not found");
            }
            
            var result = await _unitOfWork.Post.GetByCity(user.City!, request.Page, 10, cancellationToken);

            foreach (var post in result)
            {
                if (post.Responds.Any(c => c.User.Id == user.Id))
                {
                    post.AlreadyFollow = true;
                }
            }
            
            return result;        
        }
    }
}