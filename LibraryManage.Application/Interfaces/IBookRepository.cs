using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManage.Domain.Entities;

namespace LibraryManage.Application.Interfaces
{
    public interface IBookRepository
    {
        Task AddAsync(Book book, CancellationToken cancellationToken);

        Task<Book?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<Book?> GetByISBNAsync(string isbn, CancellationToken cancellationToken);

        Task<List<Book>> GetAllAsync(CancellationToken cancellationToken);

        Task UpdateAsync(Book book, CancellationToken cancellationToken);

        Task<bool> ExistsByISBNAsync(string isbn, CancellationToken cancellationToken);
    }
}