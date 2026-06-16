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
    public class AuthorRepository : IAuthorRepository
    {
        private readonly LibraryDbContext _context;

        public AuthorRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Author author, CancellationToken cancellationToken)
        {
            await _context.Authors.AddAsync(author, cancellationToken);
        }

        public async Task<Author?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Authors.FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
        }

        public async Task<List<Author>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Authors.Where(a => !a.IsDeleted).ToListAsync(cancellationToken);
        }

        public Task UpdateAsync(Author author, CancellationToken cancellationToken)
        {
            _context.Authors.Update(author);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Author author, CancellationToken cancellationToken)
        {
            _context.Authors.Update(author);
            return Task.CompletedTask;
        }

        public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Authors.AnyAsync(a => a.Id == id && !a.IsDeleted, cancellationToken);
        }
    }
}