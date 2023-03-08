using Microsoft.AspNetCore.Mvc;
using WebCatalog.Api.Models;
using WebCatalog.Logic.CQRS.Accounts.Commands.Login;
using WebCatalog.Logic.CQRS.Accounts.Commands.Register;

namespace WebCatalog.Api.Controllers;

public class AccountController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Register(RegisterDto registerDto)
    {
        var registerCommand = new RegisterCommand
        {
            UserName = registerDto.UserName,
            Password = registerDto.Password,
            Email = registerDto.Email
        };
        
        await Mediator.Send(registerCommand);

        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        var registerCommand = new LoginCommand
        {
            HaveRefreshToken = loginDto.HaveRefreshToken,
            RefreshToken = loginDto.RefreshToken,
            UserName = loginDto.UserName,
            Password = loginDto.Password
        };
        
        var registerVm = await Mediator.Send(registerCommand);

        return Ok(registerVm);
    }
    //
    // [HttpPost]
    // public async Task<IActionResult> AddRole(int userId, Role role)
    // { 
    //     await _accountService.AddRole(userId, role);
    //
    //     return Ok();
    // }
    //
    // [HttpPost]
    // public async Task<IActionResult> RemoveRole(int userId, Role role)
    // { 
    //     await _accountService.RemoveRole(userId, role);
    //
    //     return Ok();
    // }
}