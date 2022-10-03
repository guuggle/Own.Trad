using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Own.Trad.Framework.OResult;

namespace Own.Trad.WebApi.Controllers
{
#pragma warning disable CS1570 // XML comment has badly formed XML
    /// <summary>
    /// 控制器基类
    /// <remarks>
    /// 在方法上添加更多ProducesAttribute以支持更多媒体类型
    /// 相关说明
    /// <see href="https://www.youtube.com/watch?v=hn0OCI76wkk&ab_channel=RahulNath"/>
    /// 注意区别[Route("/xxx")]和[Route("xxx")]
    /// /开头则会无视combined routes
    /// </remarks>
    /// </summary>
#pragma warning restore CS1570 // XML comment has badly formed XML
    [ApiController]
    [Authorize]
    [Route("api")]
    public class ApiBaseController : ControllerBase
    {
        protected IActionResult Problem(List<OError> errors)
        {
            HttpContext.Items["errors"] = errors;
            var firstError = errors[0];
            var statusCode = firstError.Type switch
            {
                OErrorType.Conflict => StatusCodes.Status409Conflict,
                OErrorType.Validation => StatusCodes.Status400BadRequest,
                OErrorType.NotFound => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError,
            };

            return Problem(statusCode: statusCode, title: firstError.Description);
        }

        protected IActionResult Problem(OError error)
        {
            HttpContext.Items["errors"] = new List<OError>() { error };
            var statusCode = error.Type switch
            {
                OErrorType.Conflict => StatusCodes.Status409Conflict,
                OErrorType.Validation => StatusCodes.Status400BadRequest,
                OErrorType.NotFound => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError,
            };

            return Problem(statusCode: statusCode, title: error.Description);
        }
    }
}
