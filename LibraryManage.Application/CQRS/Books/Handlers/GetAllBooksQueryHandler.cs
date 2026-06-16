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
    public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, List<BookDTO>>
    {
        private readonly IBookQueryService _bookQueryService;

        public GetAllBooksQueryHandler(IBookQueryService bookQueryService)
        {
            _bookQueryService = bookQueryService ?? throw new DomainException(nameof(bookQueryService));
        }

        public async Task<List<BookDTO>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            return await _bookQueryService.GetAllAsync(cancellationToken);
        }
    }
}