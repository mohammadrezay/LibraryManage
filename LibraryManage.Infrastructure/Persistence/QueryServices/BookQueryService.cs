using LibraryManage.Application.CQRS.Books.DTOs;
using LibraryManage.Application.Interfaces;
using LibraryManage.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManage.Infrastructure.Persistence.QueryServices
{
    public class BookQueryService : IBookQueryService
    {
        private readonly LibraryDbContext _context;

        public BookQueryService(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<List<BookDTO>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Books
                .AsNoTracking()
                .OrderBy(b => b.Title)
                .Select(b => new BookDTO
                {
                    Id = b.Id,
                    Title = b.Title,
                    AuthorId = b.AuthorId,
                    PublisherId = b.PublisherId,
                    PublicationDate = b.PublicationDate,
                    TotalCopies = b.TotalCopies,
                    AvailableCopies = b.AvailableCopies,
                    Language = b.Language,
                    Category = b.Category,
                    ISBN = b.ISBN,
                    Description = b.Description
                })
                .ToListAsync(cancellationToken);
        }

        public async Task<BookDTO?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Books
                .AsNoTracking()
                .Where(b => b.Id == id)
                .Select(b => new BookDTO
                {
                    Id = b.Id,
                    Title = b.Title,
                    AuthorId = b.AuthorId,
                    PublisherId = b.PublisherId,
                    PublicationDate = b.PublicationDate,
                    TotalCopies = b.TotalCopies,
                    AvailableCopies = b.AvailableCopies,
                    Language = b.Language,
                    Category = b.Category,
                    ISBN = b.ISBN,
                    Description = b.Description
                })
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<BookDTO?> GetByISBNAsync(string isbn, CancellationToken cancellationToken)
        {
            return await _context.Books
                .AsNoTracking()
                .Where(b => b.ISBN == isbn)
                .Select(b => new BookDTO
                {
                    Id = b.Id,
                    Title = b.Title,
                    AuthorId = b.AuthorId,
                    PublisherId = b.PublisherId,
                    PublicationDate = b.PublicationDate,
                    TotalCopies = b.TotalCopies,
                    AvailableCopies = b.AvailableCopies,
                    Language = b.Language,
                    Category = b.Category,
                    ISBN = b.ISBN,
                    Description = b.Description
                })
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}