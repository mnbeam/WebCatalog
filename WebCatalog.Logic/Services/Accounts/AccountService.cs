using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebCatalog.Domain.Consts;
using WebCatalog.Domain.Entities;
using WebCatalog.Logic.Configurations;
using WebCatalog.Logic.ExternalServices;
using WebCatalog.Logic.Services.Accounts.Dtos;
using WebCatalog.Logic.Services.Tokens;
using WebCatalog.Logic.Services.Tokens.Dtos;
using WebCatalog.Logic.Validators;

namespace WebCatalog.Logic.Services.Accounts;

internal class AccountService : IAccountService
{
    private readonly AuthOptions _authOptions;
    private readonly AppDbContext _dbContext;
    private readonly ITokenService _tokenService;
    private readonly UserManager<AppUser> _userManager;
    private readonly AuthValidator _authValidator;

    public AccountService(UserManager<AppUser> userManager,
        AppDbContext dbContext,
        ITokenService tokenService,
        IOptions<AuthOptions> authOptions, 
        AuthValidator authValidator)
    {
        _userManager = userManager;
        _dbContext = dbContext;
        _tokenService = tokenService;
        _authValidator = authValidator;
        _authOptions = authOptions.Value;
    }

    public async Task Register(RegisterDto registerDto)
    {
        if (!_authValidator.IsValidRegisterData(registerDto))
        {
            throw new ArgumentException("invalid register data");
        }

        var user = new AppUser
        {
            UserName = registerDto.UserName,
            Email = registerDto.Email.ToLower()
        };

        var createResult = await _userManager.CreateAsync(user, registerDto.Password);

        if (!createResult.Succeeded)
        {
            throw new Exception("Customer creation error");
        }

        var roleResult = await _userManager.AddToRoleAsync(user, Roles.Customer);

        if (!roleResult.Succeeded)
        {
            await _userManager.DeleteAsync(user);

            throw new Exception("Adding role error");
        }
    }

    public async Task<TokenDto> Login(LoginDto loginDto)
    {
        if (loginDto.HaveRefreshToken)
        {
            return await LoginByRefreshToken(loginDto);
        }

        return await LoginByPassword(loginDto);
    }

    private async Task<TokenDto> LoginByRefreshToken(LoginDto loginDto)
    {
        var user = await GetUserByRefreshToken(loginDto.RefreshToken);

        var token = await _dbContext.Tokens.FirstOrDefaultAsync(token =>
            token.UserId == user.Id
            && token.Client == _authOptions.Audience
            && token.Value == loginDto.RefreshToken);

        if (token == null || token.ExpireTime < DateTime.Now)
        {
            throw new ArgumentException("InvalidRefreshToken");
        }

        return new TokenDto
        {
            AccessToken = _tokenService.CreateAccessToken(user),
            RefreshToken = await _tokenService.CreateRefreshToken(user.Id)
        };
    }

    private async Task<AppUser> GetUserByRefreshToken(string tokenValue)
    {
        var dividerIndex = tokenValue.IndexOf('-');

        if (dividerIndex < 0)
        {
            throw new Exception("InvalidRefreshToken");
        }

        var id = tokenValue[..dividerIndex];

        var user = await _userManager.FindByIdAsync(id);

        if (user == null)
        {
            throw new Exception("InvalidRefreshToken");
        }

        return user;
    }

    private async Task<TokenDto> LoginByPassword(LoginDto loginDto)
    {
        if (!_authValidator.IsValidUserName(loginDto.UserName))
        {
            throw new Exception("Invalid UserName");
        }

        var user = await _userManager.FindByNameAsync(loginDto.UserName);

        if (user == null ||
            !await _userManager.CheckPasswordAsync(user, loginDto.Password))
        {
            throw new Exception("IncorrectUserNameOrPassword");
        }

        return new TokenDto
        {
            AccessToken = _tokenService.CreateAccessToken(user),
            RefreshToken = await _tokenService.CreateRefreshToken(user.Id)
        };
    }
}