using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebCatalog.Domain.Entities;
using WebCatalog.Logic.Common.Configurations;
using WebCatalog.Logic.Common.ExternalServices;

namespace WebCatalog.Logic.WebCatalog.Tokens.Commands.CreateRefreshToken;

internal class CreateRefreshTokenCommandHandler : IRequestHandler<CreateRefreshTokenCommand, string>
{
    private readonly AuthOptions _authOptions;
    private readonly IDateTimeService _dateTimeService;
    private readonly AppDbContext _dbContext;

    public CreateRefreshTokenCommandHandler(AppDbContext dbContext,
        IOptions<AuthOptions> authOptions,
        IDateTimeService dateTimeService)
    {
        _dbContext = dbContext;
        _dateTimeService = dateTimeService;
        _authOptions = authOptions.Value;
    }

    public async Task<string> Handle(CreateRefreshTokenCommand request,
        CancellationToken cancellationToken)
    {
        var token = await _dbContext.Tokens.FirstOrDefaultAsync(token =>
            token.UserId == request.UserId
            && token.Client == _authOptions.Audience, cancellationToken);

        var expireTime = _dateTimeService.Now.AddDays(_authOptions.ExpireTimeRefreshTokenDays);

        var tokenValue = $"{request.UserId}-{Guid.NewGuid():D}";

        if (token == null)
        {
            var newToken = new Token
            {
                Client = _authOptions.Audience, ExpireTime = expireTime, UserId = request.UserId,
                Value = tokenValue
            };

            _dbContext.Tokens.Add(newToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return newToken.Value;
        }

        token.ExpireTime = expireTime;
        token.Value = tokenValue;
        token.UpdatedTime = _dateTimeService.Now;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return token.Value;
    }
}