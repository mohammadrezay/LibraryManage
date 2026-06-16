using LibraryManage.Application.CQRS.Books.Commands;
using LibraryManage.Application.CQRS.Books.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace LibraryManage.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllBooksQuery(), cancellationToken);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetBookByIdQuery { Id = id }, cancellationToken);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("isbn/{isbn}")]
        public async Task<IActionResult> GetByISBN(string isbn, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetBookByISBNQuery { ISBN = isbn }, cancellationToken);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBookCommand command, CancellationToken cancellationToken)
        {
            var id = await _mediator.Send(command, cancellationToken);

            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateBookCommand command, CancellationToken cancellationToken)
        {
            if (id != command.Id)
            {
                return BadRequest("Id mismatch");
            }

            await _mediator.Send(command, cancellationToken);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteBookCommand { Id = id }, cancellationToken);

            return NoContent();
        }

        [HttpPost("{id}/increase")]
        public async Task<IActionResult> IncreaseCopies(
            Guid id,
            [FromBody] int amount,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(new IncreaseBookCopiesCommand
            {
                Id = id,
                Amount = amount
            }, cancellationToken);

            return NoContent();
        }

        [HttpPost("{id}/decrease")]
        public async Task<IActionResult> DecreaseCopies(
            Guid id,
            [FromBody] int amount,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(new DecreaseBookCopiesCommand
            {
                Id = id,
                Amount = amount
            }, cancellationToken);

            return NoContent();
        }
    }
}