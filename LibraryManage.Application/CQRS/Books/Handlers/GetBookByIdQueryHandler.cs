using LibraryManage.Application.CQRS.Books.Commands;
using LibraryManage.Application.CQRS.Books.DTOs;
using LibraryManage.Application.CQRS.Books.Queries;
using LibraryManage.Application.Interfaces;
using LibraryManage.Domain.Entities;
using LibraryManage.Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManage.Application.CQRS.Books.Handlers
{
    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, BookDTO?>
    {
        private readonly IBookQueryService _queryService;

        public GetBookByIdQueryHandler(IBookQueryService queryService)
        {
            _queryService = queryService;
        }

        public async Task<BookDTO?> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            return await _queryService.GetByIdAsync(request.Id, cancellationToken);
        }
    }
}