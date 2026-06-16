using LibraryManage.Application.CQRS.Books.Commands;
using LibraryManage.Application.Interfaces;
using LibraryManage.Domain.Entities;
using LibraryManage.Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManage.Application.CQRS.Books.Handlers
{
    public class UpdateBookCommandHandler
        : IRequestHandler<UpdateBookCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateBookCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _unitOfWork.BookRepository
                .GetByIdAsync(request.Id, cancellationToken);

            if (book == null)
            {
                throw new DomainException("Book not found.");
            }

            book.UpdateBook(
                request.Title,
                request.AuthorId,
                request.PublisherId,
                request.PublicationDate,
                request.TotalCopies,
                request.Language,
                request.Category,
                request.ISBN,
                request.Description
            );

            await _unitOfWork.BookRepository.UpdateAsync(book, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}