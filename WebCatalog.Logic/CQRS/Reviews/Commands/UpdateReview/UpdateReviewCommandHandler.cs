using MediatR;
using Microsoft.EntityFrameworkCore;
using WebCatalog.Domain.Entities.ProductEntities;
using WebCatalog.Logic.Configurations;
using WebCatalog.Logic.Exceptions;
using WebCatalog.Logic.ExternalServices;

namespace WebCatalog.Logic.CQRS.Reviews.Commands.UpdateReview;

public class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand>
{
    private readonly AppDbContext _dbContext;
    private readonly IUserAccessor _userAccessor;

    public UpdateReviewCommandHandler(AppDbContext dbContext, IUserAccessor userAccessor)
    {
        _dbContext = dbContext;
        _userAccessor = userAccessor;
    }

    public async Task Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
    {
        var review = await _dbContext.Reviews
            .FirstOrDefaultAsync(r => r.Id == request.ReviewId, cancellationToken);

        if (review == null || review.UserId != _userAccessor.UserId)
        {
            throw new NotFoundException(nameof(Review), request.ReviewId);
        }

        review.Content = request.Content;
        review.Rating = request.Rating;
        review.EditedTime = DateTime.Now;

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}