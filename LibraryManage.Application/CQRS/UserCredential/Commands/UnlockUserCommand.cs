using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManage.Application.CQRS.UserCredential.Commands
{
    public class UnlockUserCommand : IRequest
    {
        public Guid UserId { get; set; }
    }
}