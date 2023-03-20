using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebCatalog.Logic.WebCatalog.Accounts.Commands.AddRole;
using WebCatalog.Logic.WebCatalog.Accounts.Commands.Login;
using WebCatalog.Logic.WebCatalog.Accounts.Commands.Register;
using WebCatalog.Logic.WebCatalog.Accounts.Commands.RemoveRole;
using WebCatalog.Logic.WebCatalog.Accounts.Queries.GetAppUserList;

namespace WebCatalog.Api.Controllers;

public class AccountController : BaseController
{
    [Authorize(Policy = "ForAdmin")]
    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var userListVm = await Mediator.Send(new GetAppUserListQuery());

        return Ok(userListVm);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterCommand registerCommand)
    {
        await Mediator.Send(registerCommand);

        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginCommand loginCommand)
    {
        var loginVm = await Mediator.Send(loginCommand);

        return Ok(loginVm);
    }

    [Authorize(Policy = "ForAdmin")]
    [HttpPost("roles")]
    public async Task<IActionResult> AddRole(AddRoleCommand addRoleCommand)
    {
        await Mediator.Send(addRoleCommand);

        return Ok();
    }

    [Authorize(Policy = "ForAdmin")]
    [HttpDelete("roles")]
    public async Task<IActionResult> RemoveRole(RemoveRoleCommand removeRoleCommand)
    {
        await Mediator.Send(removeRoleCommand);

        return Ok();
    }
}