using LibraryManage.Application.CQRS.Books.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManage.Application.Interfaces
{
    public interface IBookQueryService
    {
        Task<List<BookDTO>> GetAllAsync(CancellationToken cancellationToken);

        Task<BookDTO?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<BookDTO?> GetByISBNAsync(string isbn, CancellationToken cancellationToken);
    }
}