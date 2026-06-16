using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace LibraryManage.Application.CQRS.Authors.Commands
{
    public class DeleteAuthorCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}