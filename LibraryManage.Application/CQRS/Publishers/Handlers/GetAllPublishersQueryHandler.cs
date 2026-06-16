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
    public class GetAllPublishersQueryHandler : IRequestHandler<GetAllPublishersQuery, List<PublisherDTO>>
    {
        private readonly IPublisherQueryService _queryService;

        public GetAllPublishersQueryHandler(IPublisherQueryService queryService)
        {
            _queryService = queryService;
        }

        public async Task<List<PublisherDTO>> Handle(GetAllPublishersQuery request, CancellationToken cancellationToken)
        {
            return await _queryService.GetAllAsync(cancellationToken);
        }
    }
}