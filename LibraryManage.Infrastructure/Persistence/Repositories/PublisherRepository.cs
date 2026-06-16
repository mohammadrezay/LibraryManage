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
    public class PublisherRepository : IPublisherRepository
    {
        private readonly LibraryDbContext _context;

        public PublisherRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Publisher publisher, CancellationToken cancellationToken = default)
        {
            await _context.Publishers.AddAsync(publisher, cancellationToken);
        }

        public async Task<Publisher?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Publishers.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }

        public async Task<List<Publisher>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Publishers.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<List<Publisher>> GetActivePublishersAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Publishers.AsNoTracking().Where(p => !p.IsDeleted).ToListAsync(cancellationToken);
        }

        public Task UpdateAsync(Publisher publisher, CancellationToken cancellationToken = default)
        {
            _context.Publishers.Update(publisher);
            return Task.CompletedTask;
        }
    }
}