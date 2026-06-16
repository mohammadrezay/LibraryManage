using LibraryManage.Domain.Enums;
using LibraryManage.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryManage.Domain.Entities
{
    public class Loan
    {
        private const decimal FinePerDay = 240000m;
        private const int LoanPeriodInDays = 30;

        private readonly List<PaymentHistory> _paymentHistories = new();

        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public Guid BookId { get; private set; }
        public DateTime LoanDate { get; private set; }
        public DateTime DueDate { get; private set; }
        public DateTime? ReturnDate { get; private set; }
        public LoanStatus Status { get; private set; }
        public decimal TotalFine { get; private set; }

        public IReadOnlyCollection<PaymentHistory> PaymentHistories => _paymentHistories.AsReadOnly();

        private Loan() { }

        public Loan(Guid userId, Guid bookId, DateTime loanDate)
        {
            if(userId == Guid.Empty)
            {
                throw new DomainException("User ID is required.");
            }

            if(bookId == Guid.Empty)
            {
                throw new DomainException("Book ID is required.");
            }

            if(loanDate == default)
            {
                throw new DomainException("Loan date is required.");
            }

            Id = Guid.NewGuid();
            UserId = userId;
            BookId = bookId;
            LoanDate = loanDate;
            DueDate = loanDate.AddDays(LoanPeriodInDays);
            Status = LoanStatus.Active;
            TotalFine = 0m;
        }

        public void ReturnBook(DateTime returnDate)
        {
            if (Status == LoanStatus.Returned)
            {
                throw new DomainException("Book has already been returned.");
            }

            if (returnDate == default)
            {
                throw new DomainException("Return date is required.");
            }

            if (returnDate < LoanDate)
            {
                throw new DomainException("Return date cannot be before loan date.");
            }

            ReturnDate = returnDate;
            Status = LoanStatus.Returned;

            CalculateFine();
        }

        private void CalculateFine()
        {
            if(ReturnDate == null)
            {
                throw new DomainException("ReturnDate is required.");
            }

            if(ReturnDate.Value <= DueDate)
            {
                TotalFine = 0;
                return;
            }

            var daysOverdue = (ReturnDate.Value - DueDate).Days;
            TotalFine = daysOverdue * FinePerDay;
        }

        public void RecordPayment(decimal amount, DateTime paymentDate)
        {
            if (Status != LoanStatus.Returned)
            {
                throw new DomainException("Cannot record payment before book is returned.");
            }

            if (amount <= 0)
            {
                throw new DomainException("Payment amount must be positive.");
            }

            if(paymentDate == default)
            {
                throw new DomainException("Payment date is required.");
            }

            if(paymentDate < LoanDate)
            {
                throw new DomainException("Payment date cannot be before loan date.");
            }

            if (paymentDate < ReturnDate!.Value)
            {
                throw new DomainException("Payment date cannot be before return date.");
            }

            var remainingFine = GetRemainingFine();

            if(remainingFine <= 0)
            {
                throw new DomainException("There is no remaining fine to pay.");
            }

            if(amount > remainingFine)
            {
                throw new DomainException("Payment amount cannot exceed remaining fine.");
            }

            _paymentHistories.Add(new PaymentHistory(amount, paymentDate));
        }

        public decimal GetRemainingFine()
        {
            var paid = _paymentHistories.Sum(p => p.Amount);
            var remaining = TotalFine - paid;

            return Math.Max(0, remaining);
        }

        public int GetOverdueDays(DateTime currentDate)
        {
            if (currentDate == default)
            {
                throw new DomainException("Current date is required.");
            }

            if (Status == LoanStatus.Returned)
            {
                return 0;
            }

            if (currentDate <= DueDate)
            {
                return 0;
            }

            return (currentDate - DueDate).Days;
        }

        public decimal CalculateCurrentFine(DateTime currentDate)
        {
            if (currentDate == default)
            {
                throw new DomainException("Current date is required.");
            }

            if (Status == LoanStatus.Returned)
            {
                return TotalFine;
            }

            return GetOverdueDays(currentDate) * FinePerDay;
        }
    }
}