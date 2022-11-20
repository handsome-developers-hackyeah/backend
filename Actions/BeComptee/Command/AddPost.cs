using System.Globalization;
using Comptee.DataAccess;
using Comptee.DataAccess.Entities;
using Comptee.Exceptions;
using FluentValidation;
using MediatR;

namespace Comptee.Actions.BeComptee.Command;

public static class AddBeComptee
{
    public sealed record AddPostCommand(Guid UserId, int Ammount, string Localization, string Photo) : IRequest<Unit>;

    public class Handler : IRequestHandler<AddPostCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly string? _pathToImages;
        public Handler(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _pathToImages = configuration["PathToImages"];
        }

        public async Task<Unit> Handle(AddPostCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(request.UserId, cancellationToken);
            if (user is null)
            {
                throw new EntityNotFoundException("User not found");
            }
            try
            {
                await File.WriteAllTextAsync( _pathToImages + @"/" + user.Id + ".txt", request.Photo, cancellationToken);
            }
            catch (Exception e)
            {
                await File.WriteAllTextAsync($@"/{user.Id}", request.Photo, cancellationToken);
            }

            string date = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            
            await _unitOfWork.Post.AddAsync(new Post
            {
                Amount = request.Ammount,
                Date =  date,
                Localization = request.Localization,
                ReportCount = 0,
                UserId = request.UserId,
            }, cancellationToken);

            try
            {
                await _unitOfWork.SaveChangesAsync(cancellationToken);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return Unit.Value;
        }

        public sealed class Validator : AbstractValidator<AddPostCommand>
        {
            public Validator()
            {
                RuleFor(c => c.UserId).NotEqual(Guid.Empty);
            }
        }
    }
}