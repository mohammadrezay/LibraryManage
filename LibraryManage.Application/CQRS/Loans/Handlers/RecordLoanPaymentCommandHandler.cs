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
    public class RecordLoanPaymentCommandHandler : IRequestHandler<RecordLoanPaymentCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RecordLoanPaymentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(RecordLoanPaymentCommand request, CancellationToken cancellationToken)
        {
            var loan = await _unitOfWork.LoanRepository.GetByIdAsync(request.LoanId, cancellationToken);

            if (loan == null)
            {
                throw new DomainException("Loan not found.");
            }

            loan.RecordPayment(request.Amount, request.PaymentDate);

            await _unitOfWork.LoanRepository.UpdateAsync(loan, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}