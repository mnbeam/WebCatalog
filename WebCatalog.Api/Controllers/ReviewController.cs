using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebCatalog.Logic.WebCatalog.Reviews.Commands.CreateReview;
using WebCatalog.Logic.WebCatalog.Reviews.Commands.DeleteReview;
using WebCatalog.Logic.WebCatalog.Reviews.Commands.UpdateReview;
using WebCatalog.Logic.WebCatalog.Reviews.Queries.GetReviewListByProductId;
using WebCatalog.Logic.WebCatalog.Reviews.Queries.GetReviewListByUserId;

namespace WebCatalog.Api.Controllers;

public class ReviewController : BaseController
{
    [HttpGet("byproductId/{productId}")]
    public async Task<IActionResult> GetReviewListByProductId(int productId)
    {
        var getReviewListByProductIdCommand = new GetReviewListByProductIdQuery
        {
            ProductId = productId
        };

        var reviews = await Mediator.Send(getReviewListByProductIdCommand);

        return Ok(reviews);
    }

    [HttpGet("byuserId/{userId}")]
    public async Task<IActionResult> GetReviewListByUserId(int userId)
    {
        var getReviewListByUserIdCommand = new GetReviewListByUserIdQuery
        {
            UserId = userId
        };

        var reviews = await Mediator.Send(getReviewListByUserIdCommand);

        return Ok(reviews);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateReview(CreateReviewCommand createReviewCommand)
    {
        await Mediator.Send(createReviewCommand);

        return Ok();
    }

    [Authorize]
    [HttpPut]
    public async Task<IActionResult> UpdateReview(UpdateReviewCommand updateReviewCommand)
    {
        await Mediator.Send(updateReviewCommand);

        return Ok();
    }

    [Authorize]
    [HttpDelete]
    public async Task<IActionResult> DeleteReview(DeleteReviewCommand deleteReviewCommand)
    {
        await Mediator.Send(deleteReviewCommand);

        return Ok();
    }
}