using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManage.Application.CQRS.Users.DTOs;
using MediatR;

namespace LibraryManage.Application.CQRS.Users.Queries
{
    public class GetActiveUsersQuery : IRequest<List<UserDTO>>
    {
    }
}