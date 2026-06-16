using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using LibraryManage.Application.CQRS.Publishers.DTOs;
using LibraryManage.Application.CQRS.Publishers.Queries;
using LibraryManage.Application.Interfaces;

namespace LibraryManage.Application.CQRS.Publishers.Handlers
{
    public class GetPublisherByIdQueryHandler : IRequestHandler<GetPublisherByIdQuery, PublisherDTO?>
    {
        private readonly IPublisherQueryService _queryService;

        public GetPublisherByIdQueryHandler(IPublisherQueryService queryService)
        {
            _queryService = queryService;
        }

        public async Task<PublisherDTO?> Handle(GetPublisherByIdQuery request, CancellationToken cancellationToken)
        {
            return await _queryService.GetByIdAsync(request.Id, cancellationToken);
        }
    }
}