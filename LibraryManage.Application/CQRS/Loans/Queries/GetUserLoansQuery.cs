using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using LibraryManage.Application.CQRS.Loans.DTOs;

namespace LibraryManage.Application.CQRS.Loans.Queries
{
    public class GetUserLoansQuery : IRequest<List<LoanDTO>>
    {
        public Guid UserId { get; set; }
    }
}