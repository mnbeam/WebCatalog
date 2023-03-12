using MediatR;
using WebCatalog.Domain.Entities.ProductEntities;
using WebCatalog.Logic.Common.Configurations;
using WebCatalog.Logic.Common.ExternalServices;

namespace WebCatalog.Logic.WebCatalog.Reviews.Commands.CreateReview;

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
        Console.WriteLine(_userAccessor.UserId);

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