using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using LibraryManage.Application.CQRS.Books.DTOs;

namespace LibraryManage.Application.CQRS.Books.Queries
{
    public class GetBookByISBNQuery : IRequest<BookDTO?>
    {
        public string ISBN { get; set; } = string.Empty;
    }
}