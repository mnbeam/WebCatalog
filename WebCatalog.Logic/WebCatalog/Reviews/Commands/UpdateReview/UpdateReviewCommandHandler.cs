using MediatR;
using Microsoft.EntityFrameworkCore;
using WebCatalog.Domain.Entities.ProductEntities;
using WebCatalog.Logic.Common.Configurations;
using WebCatalog.Logic.Common.Exceptions;
using WebCatalog.Logic.Common.ExternalServices;

namespace WebCatalog.Logic.WebCatalog.Reviews.Commands.UpdateReview;

public class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand>
{
    private readonly IDateTimeService _dateTimeService;
    private readonly AppDbContext _dbContext;
    private readonly IUserAccessor _userAccessor;

    public UpdateReviewCommandHandler(AppDbContext dbContext,
        IUserAccessor userAccessor,
        IDateTimeService dateTimeService)
    {
        _dbContext = dbContext;
        _userAccessor = userAccessor;
        _dateTimeService = dateTimeService;
    }

    public async Task Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
    {
        var review = await _dbContext.Reviews
            .FirstOrDefaultAsync(r => r.Id == request.ReviewId, cancellationToken);

        if (review == null || review.UserId != _userAccessor.UserId)
        {
            throw new WebCatalogNotFoundException(nameof(Review), request.ReviewId);
        }

        review.Content = request.Content;
        review.Rating = request.Rating;
        review.EditedAt = _dateTimeService.Now;

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}