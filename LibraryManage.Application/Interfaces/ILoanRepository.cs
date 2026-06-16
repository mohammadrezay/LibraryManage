using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManage.Domain.Entities;

namespace LibraryManage.Application.Interfaces
{
    public interface ILoanRepository
    {
        Task AddAsync(Loan loan, CancellationToken cancellationToken);

        Task<Loan?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<List<Loan>> GetLoansByUserAsync(Guid userId, CancellationToken cancellationToken);

        Task<List<Loan>> GetLoansByBookAsync(Guid bookId, CancellationToken cancellationToken);

        Task<List<Loan>> GetActiveLoansAsync(CancellationToken cancellationToken);

        Task<List<Loan>> GetOverdueLoansAsync(DateTime referenceDate, CancellationToken cancellationToken);

        Task UpdateAsync(Loan loan, CancellationToken cancellationToken);
    }
}