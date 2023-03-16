using MediatR;
using WebCatalog.Domain.Entities.ProductEntities;
using WebCatalog.Logic.Common.Configurations;
using WebCatalog.Logic.Common.ExternalServices;

namespace WebCatalog.Logic.WebCatalog.Reviews.Commands.CreateReview;

public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, int>
{
    private readonly IDateTimeService _dateTimeService;
    private readonly AppDbContext _dbContext;
    private readonly IUserAccessor _userAccessor;

    public CreateReviewCommandHandler(AppDbContext dbContext,
        IUserAccessor userAccessor,
        IDateTimeService dateTimeService)
    {
        _dbContext = dbContext;
        _userAccessor = userAccessor;
        _dateTimeService = dateTimeService;
    }

    public async Task<int> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        var review = new Review
        {
            ProductId = request.ProductId,
            UserId = _userAccessor.UserId,
            Rating = request.Rating,
            Content = request.Content,
            CreatedTime = _dateTimeService.Now
        };

        await _dbContext.Reviews.AddAsync(review, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return review.Id;
    }
}