using LibraryManage.Application.CQRS.Loans.Commands;
using LibraryManage.Application.Interfaces;
using LibraryManage.Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManage.Application.CQRS.Loans.Handlers
{
    public class ReturnLoanCommandHandler : IRequestHandler<ReturnLoanCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReturnLoanCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(ReturnLoanCommand request, CancellationToken cancellationToken)
        {
            var loan = await _unitOfWork.LoanRepository.GetByIdAsync(request.LoanId, cancellationToken);

            if (loan == null)
            {
                throw new DomainException("Loan not found.");
            }

            loan.ReturnBook(request.ReturnDate);

            var book = await _unitOfWork.BookRepository.GetByIdAsync(loan.BookId, cancellationToken);

            if (book != null)
            {
                book.IncreaseCopies(1);
                await _unitOfWork.BookRepository.UpdateAsync(book, cancellationToken);
            }

            await _unitOfWork.LoanRepository.UpdateAsync(loan, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}