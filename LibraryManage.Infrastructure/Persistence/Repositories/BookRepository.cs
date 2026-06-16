using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManage.Application.Interfaces;
using LibraryManage.Domain.Entities;
using LibraryManage.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace LibraryManage.Infrastructure.Persistence.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryDbContext _context;

        public BookRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Book book, CancellationToken cancellationToken)
        {
            await _context.Books.AddAsync(book, cancellationToken);
        }

        public async Task<Book?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Books.FirstOrDefaultAsync(b => b.Id == id && !b.IsDeleted, cancellationToken);
        }

        public async Task<Book?> GetByISBNAsync(string isbn, CancellationToken cancellationToken)
        {
            return await _context.Books.FirstOrDefaultAsync(b => b.ISBN == isbn && !b.IsDeleted, cancellationToken);
        }

        public async Task<List<Book>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Books.Where(b => !b.IsDeleted).ToListAsync(cancellationToken);
        }

        public Task UpdateAsync(Book book, CancellationToken cancellationToken)
        {
            _context.Books.Update(book);
            return Task.CompletedTask;
        }

        public async Task<bool> ExistsByISBNAsync(string isbn, CancellationToken cancellationToken)
        {
            return await _context.Books.AnyAsync(b => b.ISBN == isbn && !b.IsDeleted, cancellationToken);
        }
    }
}