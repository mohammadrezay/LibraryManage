using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManage.Application.CQRS.Loans.DTOs;
using LibraryManage.Application.Interfaces;
using LibraryManage.Domain.Entities;
using LibraryManage.Domain.Enums;
using LibraryManage.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace LibraryManage.Infrastructure.Persistence.QueryServices
{
    public class LoanQueryService : ILoanQueryService
    {
        private readonly LibraryDbContext _context;

        public LoanQueryService(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<List<LoanDTO>> GetActiveLoansAsync(CancellationToken cancellationToken)
        {
            var loans = await _context.Loans
                .AsNoTracking()
                .Where(l => l.Status == LoanStatus.Active)
                .ToListAsync(cancellationToken);

            return loans.Select(l => MapToDTO(l)).ToList();
        }

        public async Task<LoanDTO?> GetByIdAsync(Guid loanId, CancellationToken cancellationToken)
        {
            var loan = await _context.Loans
                .AsNoTracking()
                .FirstOrDefaultAsync(l => l.Id == loanId, cancellationToken);

            return loan == null ? null : MapToDTO(loan);
        }

        public async Task<List<LoanDTO>> GetUserLoansAsync(Guid userId, CancellationToken cancellationToken)
        {
            var loans = await _context.Loans
                .AsNoTracking()
                .Where(l => l.UserId == userId)
                .ToListAsync(cancellationToken);

            return loans.Select(l => MapToDTO(l)).ToList();
        }

        public async Task<List<LoanDTO>> GetOverdueLoansAsync(DateTime referenceDate, CancellationToken cancellationToken)
        {
            var loans = await _context.Loans
                .AsNoTracking()
                .Where(l => l.Status == LoanStatus.Active && l.DueDate < referenceDate)
                .ToListAsync(cancellationToken);

            return loans.Select(l => MapToDTO(l, referenceDate)).ToList();
        }

        private LoanDTO MapToDTO(Loan loan, DateTime? referenceDate = null)
        {
            var now = referenceDate ?? DateTime.UtcNow;

            return new LoanDTO
            {
                Id = loan.Id,
                UserId = loan.UserId,
                BookId = loan.BookId,
                LoanDate = loan.LoanDate,
                DueDate = loan.DueDate,
                ReturnDate = loan.ReturnDate,
                IsReturned = loan.Status == LoanStatus.Returned,
                TotalFine = loan.TotalFine,

                OverdueDays = loan.GetOverdueDays(now),
                PotentialFine = loan.CalculateCurrentFine(now),

                Payments = loan.PaymentHistories
                    .Select(p => new PaymentHistoryDTO
                    {
                        PaymentId = p.Id,
                        Amount = p.Amount,
                        PaymentDate = p.PaymentDate
                    }).ToList()
            };
        }
    }
}