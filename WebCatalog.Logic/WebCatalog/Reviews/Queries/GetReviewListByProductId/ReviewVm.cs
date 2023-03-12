using AutoMapper;
using WebCatalog.Domain.Entities.ProductEntities;
using WebCatalog.Logic.Common.Mappings;

namespace WebCatalog.Logic.WebCatalog.Reviews.Queries.GetReviewListByProductId;

public class ReviewVm : IMapWith<Review>
{
    public string? UserName { get; set; }

    public string? Content { get; set; }

    public int Rating { get; set; }

    public DateTime CreatedTime { get; set; }

    public DateTime? EditedTime { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Review, ReviewVm>()
            .ForMember(reviewVm => reviewVm.UserName,
                opt => opt.MapFrom(p => p.User!.UserName));
    }
}