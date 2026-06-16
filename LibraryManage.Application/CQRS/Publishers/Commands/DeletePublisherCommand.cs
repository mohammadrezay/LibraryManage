using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace LibraryManage.Application.CQRS.Publishers.Commands
{
    public class DeletePublisherCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}