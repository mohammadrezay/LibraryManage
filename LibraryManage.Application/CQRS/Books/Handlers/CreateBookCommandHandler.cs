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
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateBookCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            if (await _unitOfWork.BookRepository.ExistsByISBNAsync(request.ISBN, cancellationToken))
            {
                throw new DomainException("Book with this ISBN already exists.");
            }

            var book = new Book(
                request.Title,
                request.AuthorId,
                request.TotalCopies,
                request.PublisherId,
                request.PublicationDate,
                request.Language,
                request.Category,
                request.ISBN,
                request.Description
            );

            await _unitOfWork.BookRepository.AddAsync(book, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return book.Id;
        }
    }
}