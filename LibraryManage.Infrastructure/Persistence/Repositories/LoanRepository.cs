using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManage.Application.Interfaces;
using LibraryManage.Domain.Entities;
using LibraryManage.Domain.Enums;
using LibraryManage.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace LibraryManage.Infrastructure.Persistence.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private readonly LibraryDbContext _context;

        public LoanRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Loan loan, CancellationToken cancellationToken)
        {
            await _context.Loans.AddAsync(loan, cancellationToken);
        }

        public async Task<Loan?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Loans.Include(l => l.PaymentHistories).FirstOrDefaultAsync(l => l.Id == id, cancellationToken);
        }

        public async Task<List<Loan>> GetLoansByUserAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await _context.Loans.Where(l => l.UserId == userId).ToListAsync(cancellationToken);
        }

        public async Task<List<Loan>> GetLoansByBookAsync(Guid bookId, CancellationToken cancellationToken)
        {
            return await _context.Loans.Where(l => l.BookId == bookId).ToListAsync(cancellationToken);
        }

        public async Task<List<Loan>> GetActiveLoansAsync(CancellationToken cancellationToken)
        {
            return await _context.Loans.Where(l => l.Status == LoanStatus.Active).ToListAsync(cancellationToken);
        }

        public async Task<List<Loan>> GetOverdueLoansAsync(DateTime referenceDate, CancellationToken cancellationToken)
        {
            return await _context.Loans.Where(l => l.Status == LoanStatus.Active && l.DueDate < referenceDate).ToListAsync(cancellationToken);
        }

        public Task UpdateAsync(Loan loan, CancellationToken cancellationToken)
        {
            _context.Loans.Update(loan);
            return Task.CompletedTask;
        }
    }
}