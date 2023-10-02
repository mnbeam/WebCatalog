using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebCatalog.Logic.Common.ExternalServices;

namespace WebCatalog.Logic.WebCatalog.Accounts.Queries.GetAppUserList;

public class GetAppUserListQueryHandler : IRequestHandler<GetAppUserListQuery, AppUserListVm>
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetAppUserListQueryHandler(AppDbContext dbContext,
        IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<AppUserListVm> Handle(GetAppUserListQuery request,
        CancellationToken cancellationToken)
    {
        return new AppUserListVm
        {
            AppUsers = await _dbContext.Users
                .ProjectTo<AppUserVm>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken)
        };
    }
}