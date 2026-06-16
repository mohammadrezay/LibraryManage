using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace LibraryManage.Application.CQRS.Loans.Commands
{
    public class RecordLoanPaymentCommand : IRequest
    {
        public Guid LoanId { get; set; }
        public Guid PaymentId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}