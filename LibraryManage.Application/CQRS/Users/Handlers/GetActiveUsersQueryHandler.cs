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
    public class GetActiveUsersQueryHandler : IRequestHandler<GetActiveUsersQuery, List<UserDTO>>
    {
        private readonly IUserQueryService _queryService;

        public GetActiveUsersQueryHandler(IUserQueryService queryService)
        {
            _queryService = queryService;
        }

        public async Task<List<UserDTO>> Handle(GetActiveUsersQuery request, CancellationToken cancellationToken)
        {
            return await _queryService.GetActiveUsersAsync(cancellationToken);
        }
    }
}