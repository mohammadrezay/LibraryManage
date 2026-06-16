using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManage.Application.CQRS.Books.Commands;
using LibraryManage.Application.Interfaces;
using LibraryManage.Domain.Entities;
using LibraryManage.Domain.Exceptions;
using MediatR;

namespace LibraryManage.Application.CQRS.Books.Handlers
{
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteBookCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _unitOfWork.BookRepository.GetByIdAsync(request.Id, cancellationToken);

            if (book == null)
            {
                throw new DomainException("Book not found.");
            }

            book.Delete();

            await _unitOfWork.BookRepository.UpdateAsync(book, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}