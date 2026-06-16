using LibraryManage.Application.CQRS.Loans.Commands;
using LibraryManage.Application.CQRS.Loans.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace LibraryManage.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class LoanController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LoanController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateLoan(
            [FromBody] CreateLoanCommand command,
            CancellationToken cancellationToken)
        {
            var loanId = await _mediator.Send(command, cancellationToken);

            return Ok(new { LoanId = loanId });
        }

        [HttpPost("return")]
        public async Task<IActionResult> ReturnLoan(
            [FromBody] ReturnLoanCommand command,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);

            return NoContent();
        }

        [HttpPost("payment")]
        public async Task<IActionResult> RecordPayment(
            [FromBody] RecordLoanPaymentCommand command,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);

            return NoContent();
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetActiveLoans(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetActiveLoansQuery(), cancellationToken);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetLoanByIdQuery { LoanId = id }, cancellationToken);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserLoans(Guid userId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetUserLoansQuery { UserId = userId }, cancellationToken);

            return Ok(result);
        }

        [HttpGet("overdue")]
        public async Task<IActionResult> GetOverdueLoans(
            [FromQuery] DateTime referenceDate,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetOverdueLoansQuery
            {
                ReferenceDate = referenceDate
            }, cancellationToken);

            return Ok(result);
        }
    }
}