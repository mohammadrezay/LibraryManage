using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace LibraryManage.Application.CQRS.Loans.Commands
{
    public class ReturnLoanCommand : IRequest
    {
        public Guid LoanId { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}