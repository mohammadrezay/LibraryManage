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
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserDTO>>
    {
        private readonly IUserQueryService _queryService;

        public GetAllUsersQueryHandler(IUserQueryService queryService)
        {
            _queryService = queryService;
        }

        public async Task<List<UserDTO>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            return await _queryService.GetAllAsync(cancellationToken);
        }
    }
}