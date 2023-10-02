using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebCatalog.Api.Controllers;

/// <summary>
/// Базовый контроллер.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public abstract class BaseController : ControllerBase
{
    /// <summary>
    /// Медиатр.
    /// </summary>
    private IMediator? _mediator;

    /// <summary>
    /// Медиатр.
    /// </summary>
    protected IMediator Mediator =>
        _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();
}