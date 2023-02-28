using Microsoft.AspNetCore.Mvc;
using WebCatalog.Logic.Services.Accounts;
using WebCatalog.Logic.Services.Accounts.Dtos;

namespace WebCatalog.Api.Controllers;

[Route("[controller]/[action]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterDto registerDto)
    {
        await _accountService.Register(registerDto);

        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        var result = await _accountService.Login(loginDto);

        return Ok(result);
    }
}