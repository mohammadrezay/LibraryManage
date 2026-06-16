using LibraryManage.Application.CQRS.Users.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManage.Application.CQRS.Users.Queries
{
    public class GetUserByIdQuery : IRequest<UserDTO?>
    {
        public Guid Id { get; set; }
    }
}