using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LibraryManage.Application.CQRS.Users.Commands;
using LibraryManage.Application.CQRS.UserCredential.Commands;

namespace LibraryManage.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserCommand command)
        {
            var userId = await _mediator.Send(command);

            return Ok(new
            {
                Message = "User registered successfully",
                UserId = userId
            });
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserCommand command)
        {
            var token = await _mediator.Send(command);

            return Ok(new { Token = token });
        }

        [Authorize]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordCommand command)
        {
            await _mediator.Send(command);

            return Ok(new { Message = "Password changed successfully" });
        }

        [Authorize]
        [HttpPost("unlock")]
        public async Task<IActionResult> UnlockUser(UnlockUserCommand command)
        {
            await _mediator.Send(command);

            return Ok(new { Message = "User unlocked successfully" });
        }
    }
}