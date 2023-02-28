using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebCatalog.Domain.Entities;
using WebCatalog.Logic.Configurations;
using WebCatalog.Logic.ExternalServices;
using WebCatalog.Logic.Validators;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace WebCatalog.Logic.Services.Tokens;

internal class TokenService : ITokenService
{
    private readonly AuthOptions _authOptions;
    private readonly AppDbContext _dbContext;
    private readonly AuthValidator _authValidator;

    public TokenService(AppDbContext dbContext,
        IOptions<AuthOptions> authOptions, 
        AuthValidator authValidator)
    {
        _dbContext = dbContext;
        _authValidator = authValidator;
        _authOptions = authOptions.Value;
    }

    public string CreateAccessToken(AppUser user)
    {
        if (!_authValidator.IsValidUser(user))
        {
            throw new ArgumentException("invalid user");
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Sub, user.UserName),
            new(JwtRegisteredClaimNames.Email, user.Email)
        };

        var token = new JwtSecurityToken(
            _authOptions.Issuer,
            _authOptions.Audience,
            claims,
            DateTime.Now,
            DateTime.Now.AddMinutes(_authOptions.ExpireTimeTokenMinutes),
            new SigningCredentials(_authOptions.SymmetricSecurityKey,
                SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<string> CreateRefreshToken(int userId)
    {
        if (!_authValidator.IsValidUserId(userId))
        {
            throw new ArgumentException($"invalid userId: {userId}");
        }

        var token = await _dbContext.Tokens.FirstOrDefaultAsync(token =>
            token.UserId == userId
            && token.Client == _authOptions.Audience);

        var expireTime = DateTime.Now.AddDays(_authOptions.ExpireTimeRefreshTokenDays);

        var tokenValue = $"{userId}-{Guid.NewGuid():D}";

        if (token == null)
        {
            var newToken = new Token
            {
                Client = _authOptions.Audience,
                ExpireTime = expireTime,
                UserId = userId,
                Value = tokenValue
            };

            _dbContext.Tokens.Add(newToken);
            await _dbContext.SaveChangesAsync();

            return newToken.Value;
        }

        token.ExpireTime = expireTime;
        token.Value = tokenValue;
        token.UpdatedTime = DateTime.Now;

        await _dbContext.SaveChangesAsync();

        return token.Value;
    }
}