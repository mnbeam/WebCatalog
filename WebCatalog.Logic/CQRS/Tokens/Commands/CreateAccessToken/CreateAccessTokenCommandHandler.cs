using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebCatalog.Logic.Configurations;

namespace WebCatalog.Logic.CQRS.Tokens.Commands.CreateAccessToken;

public class CreateAccessTokenCommandHandler : IRequestHandler<CreateAccessTokenCommand, string>
{
    private readonly AuthOptions _authOptions;

    public CreateAccessTokenCommandHandler(IOptions<AuthOptions> authOptions)
    {
        _authOptions = authOptions.Value;
    }

    public async Task<string> Handle(CreateAccessTokenCommand request, CancellationToken cancellationToken)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, request.AppUser.ToString()),
            new(JwtRegisteredClaimNames.Sub, request.AppUser.UserName),
            new(JwtRegisteredClaimNames.Email, request.AppUser.Email)
        };

        var token = new JwtSecurityToken(
            issuer: _authOptions.Issuer,
            audience: _authOptions.Audience,
            claims: claims,
            notBefore: DateTime.Now,
            expires: DateTime.Now.AddMinutes(_authOptions.ExpireTimeTokenMinutes),
            signingCredentials: new SigningCredentials(_authOptions.SymmetricSecurityKey,
                SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}