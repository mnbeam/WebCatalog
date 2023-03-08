using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebCatalog.Domain.Entities;
using WebCatalog.Logic.Configurations;
using WebCatalog.Logic.ExternalServices;

namespace WebCatalog.Logic.CQRS.Tokens.Commands.CreateRefreshToken;

internal class CreateRefreshTokenCommandHandler : IRequestHandler<CreateRefreshTokenCommand, string>
{
    private readonly AppDbContext _dbContext;
    private readonly AuthOptions _authOptions;

    public CreateRefreshTokenCommandHandler(AppDbContext dbContext, IOptions<AuthOptions> authOptions)
    {
        _dbContext = dbContext;
        _authOptions = authOptions.Value;
    }

    public async Task<string> Handle(CreateRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var token = await _dbContext.Tokens.FirstOrDefaultAsync(token =>
            token.UserId == request.UserId
            && token.Client == _authOptions.Audience, cancellationToken);

        var expireTime = DateTime.Now.AddDays(_authOptions.ExpireTimeRefreshTokenDays);

        var tokenValue = $"{request.UserId}-{Guid.NewGuid():D}";

        if (token == null)
        {
            var newToken = new Token
            {
                Client = _authOptions.Audience,
                ExpireTime = expireTime,
                UserId = request.UserId,
                Value = tokenValue
            };

            _dbContext.Tokens.Add(newToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return newToken.Value;
        }

        token.ExpireTime = expireTime;
        token.Value = tokenValue;
        token.UpdatedTime = DateTime.Now;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return token.Value;
    }
}