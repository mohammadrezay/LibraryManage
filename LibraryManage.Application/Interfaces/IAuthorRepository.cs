using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManage.Domain.Entities;

namespace LibraryManage.Application.Interfaces
{
    public interface IAuthorRepository
    {
        Task AddAsync(Author author, CancellationToken cancellationToken);

        Task<Author?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<List<Author>> GetAllAsync(CancellationToken cancellationToken);

        Task UpdateAsync(Author author, CancellationToken cancellationToken);

        Task DeleteAsync(Author author, CancellationToken cancellationToken);

        Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken);
    }
}