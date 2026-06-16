using LibraryManage.Application.CQRS.Users.DTOs;
using LibraryManage.Application.CQRS.Users.Queries;
using LibraryManage.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManage.Application.CQRS.Users.Handlers
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDTO?>
    {
        private readonly IUserQueryService _queryService;

        public GetUserByIdQueryHandler(IUserQueryService queryService)
        {
            _queryService = queryService;
        }

        public async Task<UserDTO?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            return await _queryService.GetByIdAsync(request.Id, cancellationToken);
        }
    }
}