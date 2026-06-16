using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManage.Application.CQRS.Loans.DTOs
{
    public class LoanDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool IsReturned { get; set; }
        public decimal TotalFine { get; set; }

        public int OverdueDays { get; set; }
        public decimal PotentialFine { get; set; }

        public List<PaymentHistoryDTO> Payments { get; set; } = new();
    }
}