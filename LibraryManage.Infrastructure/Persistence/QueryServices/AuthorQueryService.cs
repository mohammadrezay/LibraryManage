using LibraryManage.Application.CQRS.Authors.DTOs;
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
    public class AuthorQueryService : IAuthorQueryService
    {
        private readonly LibraryDbContext _context;

        public AuthorQueryService(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<List<AuthorDTO>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Authors
                .AsNoTracking()
                .OrderBy(a => a.FullName)
                .Select(a => new AuthorDTO
                {
                    Id = a.Id,
                    FullName = a.FullName,
                    BirthDate = a.BirthDate,
                    Nationality = a.Nationality,
                    Bio = a.Bio
                })
                .ToListAsync(cancellationToken);
        }

        public async Task<AuthorDTO?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Authors
                .AsNoTracking()
                .Where(a => a.Id == id)
                .Select(a => new AuthorDTO
                {
                    Id = a.Id,
                    FullName = a.FullName,
                    BirthDate = a.BirthDate,
                    Nationality = a.Nationality,
                    Bio = a.Bio
                })
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}