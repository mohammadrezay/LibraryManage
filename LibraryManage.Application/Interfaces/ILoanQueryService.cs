using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManage.Application.CQRS.Loans.DTOs;

namespace LibraryManage.Application.Interfaces
{
    public interface ILoanQueryService
    {
        Task<List<LoanDTO>> GetActiveLoansAsync(CancellationToken cancellationToken);

        Task<LoanDTO?> GetByIdAsync(Guid loanId, CancellationToken cancellationToken);

        Task<List<LoanDTO>> GetUserLoansAsync(Guid userId, CancellationToken cancellationToken);

        Task<List<LoanDTO>> GetOverdueLoansAsync(DateTime referenceDate, CancellationToken cancellationToken);
    }
}