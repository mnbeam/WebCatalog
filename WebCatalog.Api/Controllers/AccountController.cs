using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebCatalog.Logic.WebCatalog.Accounts.Commands.AddRole;
using WebCatalog.Logic.WebCatalog.Accounts.Commands.Login;
using WebCatalog.Logic.WebCatalog.Accounts.Commands.Register;
using WebCatalog.Logic.WebCatalog.Accounts.Commands.RemoveRole;
using WebCatalog.Logic.WebCatalog.Accounts.Queries.GetAppUserList;

namespace WebCatalog.Api.Controllers;

/// <summary>
/// Контроллер для работы с аккаунтом.
/// </summary>
public class AccountController : BaseController
{
    /// <summary>
    /// Получить всех пользователей системы.
    /// </summary>
    /// <returns></returns>
    [Authorize(Policy = "ForAdmin")]
    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var userListVm = await Mediator.Send(new GetAppUserListQuery());

        return Ok(userListVm);
    }

    /// <summary>
    /// Зарегистрироваться.
    /// </summary>
    /// <param name="registerCommand"></param>
    /// <returns></returns>
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterCommand registerCommand)
    {
        await Mediator.Send(registerCommand);

        return Ok();
    }

    /// <summary>
    /// Войти.
    /// </summary>
    /// <param name="loginCommand"></param>
    /// <returns>Access и Refresh токены.</returns>
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginCommand loginCommand)
    {
        var loginVm = await Mediator.Send(loginCommand);

        return Ok(loginVm);
    }

    /// <summary>
    /// Добавить роль пользователю.
    /// </summary>
    /// <param name="addRoleCommand">Команда для добавления роли.</param>
    [Authorize(Policy = "ForAdmin")]
    [HttpPost("roles")]
    public async Task<IActionResult> AddRole(AddRoleCommand addRoleCommand)
    {
        await Mediator.Send(addRoleCommand);

        return Ok();
    }

    /// <summary>
    /// Удалить роль пользователя.
    /// </summary>
    /// <param name="removeRoleCommand">Команла для удаления роли.</param>
    [Authorize(Policy = "ForAdmin")]
    [HttpDelete("roles")]
    public async Task<IActionResult> RemoveRole(RemoveRoleCommand removeRoleCommand)
    {
        await Mediator.Send(removeRoleCommand);

        return Ok();
    }
}