using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebCatalog.Domain.Entities;
using WebCatalog.Logic.Common.Configurations;
using WebCatalog.Logic.Common.Exceptions;
using WebCatalog.Logic.Common.ExternalServices;
using WebCatalog.Logic.WebCatalog.Tokens.Commands.CreateAccessToken;
using WebCatalog.Logic.WebCatalog.Tokens.Commands.CreateRefreshToken;

namespace WebCatalog.Logic.WebCatalog.Accounts.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginVm>
{
    private readonly AuthOptions _authOptions;
    private readonly IDateTimeService _dateTimeService;
    private readonly AppDbContext _dbContext;
    private readonly IMediator _mediator;
    private readonly UserManager<AppUser> _userManager;

    public LoginCommandHandler(AppDbContext dbContext,
        UserManager<AppUser> userManager,
        IMediator mediator,
        IOptions<AuthOptions> authOptions,
        IDateTimeService dateTimeService)
    {
        _dbContext = dbContext;
        _userManager = userManager;
        _mediator = mediator;
        _dateTimeService = dateTimeService;
        _authOptions = authOptions.Value;
    }

    public async Task<LoginVm> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        if (request.HaveRefreshToken)
        {
            return await LoginByRefreshTokenAsync(request.RefreshToken);
        }

        return await LoginByPasswordAsync(request.UserName, request.Password);
    }

    private async Task<LoginVm> LoginByRefreshTokenAsync(string refreshToken)
    {
        var user = await GetUserFromRefreshTokenAsync(refreshToken);

        await CheckRefreshTokenAsync(user, refreshToken);

        return new LoginVm
        {
            AccessToken = await _mediator.Send(new CreateAccessTokenCommand {AppUser = user}),
            RefreshToken = await _mediator.Send(new CreateRefreshTokenCommand {UserId = user.Id})
        };
    }

    private async Task<AppUser> GetUserFromRefreshTokenAsync(string refreshToken)
    {
        var dividerIndex = refreshToken.IndexOf('-');

        if (dividerIndex < 0)
        {
            throw new WebCatalogValidationException("InvalidRefreshToken");
        }

        var id = refreshToken[..dividerIndex];

        var user = await _userManager.FindByIdAsync(id);

        if (user == null)
        {
            throw new WebCatalogNotFoundException(nameof(AppUser), id);
        }

        return user;
    }

    private async Task CheckRefreshTokenAsync(AppUser user, string refreshToken)
    {
        var token = await _dbContext.Tokens.FirstOrDefaultAsync(t =>
            t.UserId == user.Id
            && t.Client == _authOptions.Audience
            && t.Value == refreshToken);

        if (token == null || token.ExpireTime < _dateTimeService.Now)
        {
            throw new WebCatalogValidationException("InvalidRefreshToken");
        }
    }

    private async Task<LoginVm> LoginByPasswordAsync(string userName, string password)
    {
        var user = await _userManager.FindByNameAsync(userName);

        if (user == null || !await _userManager.CheckPasswordAsync(user, password))
        {
            //todo InvalidUserNameOrPasswordException
            throw new WebCatalogNotFoundException(nameof(AppUser), userName);
        }

        return new LoginVm
        {
            AccessToken = await _mediator.Send(new CreateAccessTokenCommand {AppUser = user}),
            RefreshToken = await _mediator.Send(new CreateRefreshTokenCommand {UserId = user.Id})
        };
    }
}