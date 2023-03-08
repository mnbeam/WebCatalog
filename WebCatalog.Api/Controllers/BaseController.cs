﻿using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebCatalog.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public abstract class BaseController : ControllerBase
{
    private IMediator _mediator;
    protected IMediator Mediator =>
        _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
}