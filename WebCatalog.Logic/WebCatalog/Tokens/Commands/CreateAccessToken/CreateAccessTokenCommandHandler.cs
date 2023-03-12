using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebCatalog.Logic.Common.Configurations;

namespace WebCatalog.Logic.WebCatalog.Tokens.Commands.CreateAccessToken;

public class CreateAccessTokenCommandHandler : IRequestHandler<CreateAccessTokenCommand, string>
{
    private readonly AuthOptions _authOptions;

    public CreateAccessTokenCommandHandler(IOptions<AuthOptions> authOptions)
    {
        _authOptions = authOptions.Value;
    }

    public async Task<string> Handle(CreateAccessTokenCommand request,
        CancellationToken cancellationToken)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, request.AppUser.Id.ToString()),
            new(JwtRegisteredClaimNames.Sub, request.AppUser.UserName!),
            new(JwtRegisteredClaimNames.Email, request.AppUser.Email!)
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
}