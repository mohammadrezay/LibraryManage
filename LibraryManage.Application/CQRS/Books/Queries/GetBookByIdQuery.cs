using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using LibraryManage.Application.CQRS.Books.DTOs;

namespace LibraryManage.Application.CQRS.Books.Queries
{
    public class GetBookByIdQuery : IRequest<BookDTO?>
    {
        public Guid Id { get; set; }
    }
}