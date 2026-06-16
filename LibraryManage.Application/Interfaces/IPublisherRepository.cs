using LibraryManage.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManage.Application.Interfaces
{
    public interface IPublisherRepository
    {
        Task AddAsync(Publisher publisher, CancellationToken cancellationToken = default);
        Task<Publisher?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<List<Publisher>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<List<Publisher>> GetActivePublishersAsync(CancellationToken cancellationToken = default);
        Task UpdateAsync(Publisher publisher, CancellationToken cancellationToken = default);
    }
}