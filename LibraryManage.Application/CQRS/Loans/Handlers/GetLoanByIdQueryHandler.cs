using LibraryManage.Application.CQRS.Loans.DTOs;
using LibraryManage.Application.CQRS.Loans.Queries;
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
    public class GetLoanByIdQueryHandler : IRequestHandler<GetLoanByIdQuery, LoanDTO?>
    {
        private readonly ILoanQueryService _loanQueryService;

        public GetLoanByIdQueryHandler(ILoanQueryService loanQueryService)
        {
            _loanQueryService = loanQueryService ?? throw new DomainException(nameof(loanQueryService));
        }

        public async Task<LoanDTO?> Handle(GetLoanByIdQuery request, CancellationToken cancellationToken)
        {
            return await _loanQueryService.GetByIdAsync(request.LoanId, cancellationToken);
        }
    }
}