using System.Data.SqlTypes;
using System.Net.Cache;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Diagnostics;
using Own.Trad.Framework.OResult;
using Microsoft.Extensions.Logging;

namespace Own.Trad.WebApi.Controllers
{
    public class ErrorsController : ApiBaseController
    {
        private readonly ILogger<ErrorsController> _logger;

        public ErrorsController(ILogger<ErrorsController> logger)
        {
            _logger = logger;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("/error")]
        public IActionResult Error()
        {
            Exception exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
            _logger.LogError($"Unexpected Error: {exception?.Message}");
            return Problem(OError.Unexpected());
        }
    }
}