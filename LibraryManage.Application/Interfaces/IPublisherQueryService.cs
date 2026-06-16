using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManage.Application.CQRS.Publishers.DTOs;

namespace LibraryManage.Application.Interfaces
{
    public interface IPublisherQueryService
    {
        Task<List<PublisherDTO>> GetAllAsync(CancellationToken cancellationToken);

        Task<PublisherDTO?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<List<PublisherDTO>> GetActivePublishersAsync(CancellationToken cancellationToken);
    }
}