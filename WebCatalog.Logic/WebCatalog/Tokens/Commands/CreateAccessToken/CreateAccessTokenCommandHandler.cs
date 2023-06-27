using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebCatalog.Domain.Entities;
using WebCatalog.Logic.Common.Configurations;
using WebCatalog.Logic.Common.ExternalServices;

namespace WebCatalog.Logic.WebCatalog.Tokens.Commands.CreateAccessToken;

public class CreateAccessTokenCommandHandler : IRequestHandler<CreateAccessTokenCommand, string>
{
    private readonly AuthOptions _authOptions;
    private readonly IDateTimeService _dateTimeService;
    private readonly UserManager<AppUser> _userManager;

    public CreateAccessTokenCommandHandler(IOptions<AuthOptions> authOptions,
        IDateTimeService dateTimeService,
        UserManager<AppUser> userManager)
    {
        _dateTimeService = dateTimeService;
        _userManager = userManager;
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

        await AddRolesToClaimsAsync(request, claims);

        var token = new JwtSecurityToken(
            _authOptions.Issuer,
            _authOptions.Audience,
            claims,
            _dateTimeService.Now,
            _dateTimeService.Now.AddMinutes(_authOptions.ExpireTimeTokenMinutes),
            new SigningCredentials(_authOptions.SymmetricSecurityKey,
                SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private async Task AddRolesToClaimsAsync(CreateAccessTokenCommand request, List<Claim> claims)
    {
        var roles = await _userManager.GetRolesAsync(request.AppUser);

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
    }
}