using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManage.Application.CQRS.Users.DTOs;
using LibraryManage.Application.Interfaces;
using MediatR;

namespace LibraryManage.Application.CQRS.Users.Queries
{
    public class GetUserByUsernameQueryHandler : IRequestHandler<GetUserByUsernameQuery, UserDTO?>
    {
        private readonly IUserQueryService _queryService;

        public GetUserByUsernameQueryHandler(IUserQueryService queryService)
        {
            _queryService = queryService;
        }

        public async Task<UserDTO?> Handle(GetUserByUsernameQuery request, CancellationToken cancellationToken)
        {
            return await _queryService.GetByUsernameAsync(request.Username, cancellationToken);
        }
    }
}