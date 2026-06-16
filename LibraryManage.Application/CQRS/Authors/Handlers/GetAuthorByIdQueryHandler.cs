using LibraryManage.Application.CQRS.Authors.DTOs;
using LibraryManage.Application.CQRS.Authors.Queries;
using LibraryManage.Application.Interfaces;
using LibraryManage.Domain.Entities;
using LibraryManage.Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryManage.Application.CQRS.Authors.Handlers
{
    public class GetAuthorByIdQueryHandler : IRequestHandler<GetAuthorByIdQuery, AuthorDTO?>
    {
        private readonly IAuthorQueryService _authorQueryService;

        public GetAuthorByIdQueryHandler(IAuthorQueryService authorQueryService)
        {
            _authorQueryService = authorQueryService ?? throw new DomainException(nameof(authorQueryService));
        }

        public async Task<AuthorDTO?> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
        {
            return await _authorQueryService
                .GetByIdAsync(request.Id, cancellationToken);
        }
    }
}