using LibraryManage.Application.CQRS.Books.DTOs;
using LibraryManage.Application.CQRS.Books.Queries;
using LibraryManage.Application.Interfaces;
using LibraryManage.Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManage.Application.CQRS.Books.Handlers
{
    public class GetBookByISBNQueryHandler : IRequestHandler<GetBookByISBNQuery, BookDTO?>
    {
        private readonly IBookQueryService _queryService;

        public GetBookByISBNQueryHandler(IBookQueryService queryService)
        {
            _queryService = queryService ?? throw new DomainException(nameof(queryService));
        }

        public async Task<BookDTO?> Handle(GetBookByISBNQuery request, CancellationToken cancellationToken)
        {
            return await _queryService.GetByISBNAsync(request.ISBN, cancellationToken);
        }
    }
}