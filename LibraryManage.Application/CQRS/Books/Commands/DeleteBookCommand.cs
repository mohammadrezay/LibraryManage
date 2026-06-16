using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace LibraryManage.Application.CQRS.Books.Commands
{
    public class DeleteBookCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}