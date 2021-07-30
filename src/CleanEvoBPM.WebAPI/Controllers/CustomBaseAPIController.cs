﻿using CleanEvoBPM.Application.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace CleanEvoBPM.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class CustomBaseApiController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}
