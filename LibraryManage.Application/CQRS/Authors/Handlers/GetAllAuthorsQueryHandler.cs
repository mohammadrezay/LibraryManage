using LibraryManage.Application.CQRS.Authors.DTOs;
using LibraryManage.Application.CQRS.Authors.Queries;
using LibraryManage.Application.Interfaces;
using LibraryManage.Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManage.Application.CQRS.Authors.Handlers
{
    public class GetAllAuthorsQueryHandler : IRequestHandler<GetAllAuthorsQuery, List<AuthorDTO>>
    {
        private readonly IAuthorQueryService _authorQueryService;

        public GetAllAuthorsQueryHandler(IAuthorQueryService authorQueryService)
        {
            _authorQueryService = authorQueryService ?? throw new DomainException(nameof(authorQueryService));
        }

        public async Task<List<AuthorDTO>> Handle(GetAllAuthorsQuery request, CancellationToken cancellationToken)
        {
            return await _authorQueryService.GetAllAsync(cancellationToken);
        }
    }
}