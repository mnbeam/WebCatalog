using MediatR;
using Microsoft.EntityFrameworkCore;
using WebCatalog.Domain.Entities.ProductEntities;
using WebCatalog.Logic.Common.Configurations;
using WebCatalog.Logic.Common.Exceptions;
using WebCatalog.Logic.Common.ExternalServices;

namespace WebCatalog.Logic.WebCatalog.Reviews.Commands.DeleteReview;

public class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand>
{
    private readonly AppDbContext _dbContext;
    private readonly IUserAccessor _userAccessor;

    public DeleteReviewCommandHandler(AppDbContext dbContext, IUserAccessor userAccessor)
    {
        _dbContext = dbContext;
        _userAccessor = userAccessor;
    }

    public async Task Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
    {
        var review = await _dbContext.Reviews
            .FirstOrDefaultAsync(r => r.Id == request.ReviewId, cancellationToken);

        if (review == null || review.UserId != _userAccessor.UserId)
        {
            throw new WebCatalogNotFoundException(nameof(Review), request.ReviewId);
        }

        _dbContext.Reviews.Remove(review);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}