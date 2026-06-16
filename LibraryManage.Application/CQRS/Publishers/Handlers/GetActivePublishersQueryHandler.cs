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
    public class GetActivePublishersQueryHandler : IRequestHandler<GetActivePublishersQuery, List<PublisherDTO>>
    {
        private readonly IPublisherQueryService _queryService;

        public GetActivePublishersQueryHandler(IPublisherQueryService queryService)
        {
            _queryService = queryService;
        }

        public async Task<List<PublisherDTO>> Handle(GetActivePublishersQuery request, CancellationToken cancellationToken)
        {
            return await _queryService.GetActivePublishersAsync(cancellationToken);
        }
    }
}