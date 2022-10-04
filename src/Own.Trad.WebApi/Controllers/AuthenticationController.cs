using System;
using System.Threading.Tasks;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Own.Trad.Services.Interfaces;
using Own.Trad.WebApi.Contracts.Authentication;

namespace Own.Trad.WebApi.Controllers
{
    [AllowAnonymous]
    [Route("api/auth")]
    public class AuthenticationController : ApiBaseController
    {
        private readonly IAuthService _service;
        private readonly IMapper _mapper;

        public AuthenticationController(IAuthService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var authResult = await _service.Register(request.Email, request.UserName, request.Password);

            return authResult.Match(
                authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
                errors => Problem(errors)
            );
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var authResult = await _service.Login(request.Email, request.Password);
            return authResult.Match(
                authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
                errors => Problem(errors)
            );
        }

    }
}
