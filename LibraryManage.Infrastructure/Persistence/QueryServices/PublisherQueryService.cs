using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManage.Application.CQRS.Publishers.DTOs;
using LibraryManage.Application.Interfaces;
using LibraryManage.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace LibraryManage.Infrastructure.Persistence.QueryServices
{
    public class PublisherQueryService : IPublisherQueryService
    {
        private readonly LibraryDbContext _context;

        public PublisherQueryService(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<List<PublisherDTO>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Publishers
                .AsNoTracking()
                .Select(p => new PublisherDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Country = p.Country,
                    Description = p.Description,
                    IsDeleted = p.IsDeleted
                })
                .ToListAsync(cancellationToken);
        }

        public async Task<PublisherDTO?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Publishers
                .AsNoTracking()
                .Where(p => p.Id == id)
                .Select(p => new PublisherDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Country = p.Country,
                    Description = p.Description,
                    IsDeleted = p.IsDeleted
                })
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<List<PublisherDTO>> GetActivePublishersAsync(CancellationToken cancellationToken)
        {
            return await _context.Publishers
                .AsNoTracking()
                .Where(p => !p.IsDeleted)
                .Select(p => new PublisherDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Country = p.Country,
                    Description = p.Description,
                    IsDeleted = p.IsDeleted
                })
                .ToListAsync(cancellationToken);
        }
    }
}