using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using LibraryManage.Application.CQRS.Publishers.DTOs;

namespace LibraryManage.Application.CQRS.Publishers.Queries
{
    public class GetActivePublishersQuery : IRequest<List<PublisherDTO>>
    {
    }
}