using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LibraryManage.Application.CQRS.UserCredential.Commands;

namespace LibraryManage.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserCredentialController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserCredentialController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(
            [FromBody] ChangePasswordCommand command,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);

            return NoContent();
        }

        [HttpPost("unlock")]
        public async Task<IActionResult> Unlock(
            [FromBody] UnlockUserCommand command,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);

            return NoContent();
        }
    }
}