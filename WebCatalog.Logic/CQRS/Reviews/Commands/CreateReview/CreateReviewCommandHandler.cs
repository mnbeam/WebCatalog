using MediatR;
using WebCatalog.Domain.Entities.ProductEntities;
using WebCatalog.Logic.Configurations;
using WebCatalog.Logic.ExternalServices;

namespace WebCatalog.Logic.CQRS.Reviews.Commands.CreateReview;

public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, int>
{
    private readonly AppDbContext _dbContext;
    private readonly IUserAccessor _userAccessor;

    public CreateReviewCommandHandler(AppDbContext dbContext, IUserAccessor userAccessor)
    {
        _dbContext = dbContext;
        _userAccessor = userAccessor;
    }

    public async Task<int> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        var review = new Review
        {
            ProductId = request.ProductId,
            UserId = _userAccessor.UserId,
            Rating = request.Rating,
            Content = request.Content,
            CreatedTime = DateTime.Now
        };

        await _dbContext.Reviews.AddAsync(review, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return review.Id;
    }
}