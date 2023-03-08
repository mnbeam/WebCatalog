using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebCatalog.Domain.Entities;
using WebCatalog.Logic.Configurations;
using WebCatalog.Logic.CQRS.Tokens.Commands.CreateAccessToken;
using WebCatalog.Logic.CQRS.Tokens.Commands.CreateRefreshToken;
using WebCatalog.Logic.Exceptions;
using WebCatalog.Logic.ExternalServices;

namespace WebCatalog.Logic.CQRS.Accounts.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginVm>
{
    private readonly AppDbContext _dbContext;
    private readonly UserManager<AppUser> _userManager;
    private readonly IMediator _mediator;
    private readonly AuthOptions _authOptions;

    public LoginCommandHandler(AppDbContext dbContext,
        UserManager<AppUser> userManager,
        IMediator mediator,
        IOptions<AuthOptions> authOptions)
    {
        _dbContext = dbContext;
        _userManager = userManager;
        _mediator = mediator;
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
            throw new Exception("InvalidRefreshToken"); 
        }

        var id = refreshToken[..dividerIndex];

        var user = await _userManager.FindByIdAsync(id);

        if (user == null)
        {
            throw new NotFoundException(nameof(AppUser), id);
        }

        return user;
    }
    
    private async Task CheckRefreshTokenAsync(AppUser user, string refreshToken)
    {
        var token = await _dbContext.Tokens.FirstOrDefaultAsync(t =>
            t.UserId == user.Id
            && t.Client == _authOptions.Audience
            && t.Value == refreshToken);

        if (token == null || token.ExpireTime < DateTime.Now)
        {
            throw new Exception("InvalidRefreshToken");
        }
    }

    private async Task<LoginVm> LoginByPasswordAsync(string userName, string password)
    {
        var user = await _userManager.FindByNameAsync(userName);
        
        if (user == null || !await _userManager.CheckPasswordAsync(user, password))
        { //todo InvalidUserNameOrPasswordException
            throw new NotFoundException(nameof(AppUser), userName);
        }

        return new LoginVm
        {
            AccessToken = await _mediator.Send(new CreateAccessTokenCommand {AppUser = user}),
            RefreshToken = await _mediator.Send(new CreateRefreshTokenCommand {UserId = user.Id})
        };
    }
}