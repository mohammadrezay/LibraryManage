using LibraryManage.Application.CQRS.Loans.Commands;
using LibraryManage.Application.Interfaces;
using LibraryManage.Domain.Entities;
using LibraryManage.Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManage.Application.CQRS.Loans.Handlers
{
    public class CreateLoanCommandHandler : IRequestHandler<CreateLoanCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateLoanCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateLoanCommand request, CancellationToken cancellationToken)
        {
            var book = await _unitOfWork.BookRepository.GetByIdAsync(request.BookId, cancellationToken);

            if (book == null)
            {
                throw new DomainException("Book not found.");
            }

            if (book.AvailableCopies <= 0)
            {
                throw new DomainException("No available copies for this book.");
            }

            book.DecreaseCopies(1);

            var loan = new Loan(
                request.UserId,
                request.BookId,
                request.LoanDate
            );

            await _unitOfWork.LoanRepository.AddAsync(loan, cancellationToken);
            await _unitOfWork.BookRepository.UpdateAsync(book, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return loan.Id;
        }
    }
}