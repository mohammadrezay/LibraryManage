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
    public class UserCredentialRepository : IUserCredentialRepository
    {
        private readonly LibraryDbContext _context;

        public UserCredentialRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(UserCredential userCredential, CancellationToken cancellationToken)
        {
            await _context.UserCredentials.AddAsync(userCredential, cancellationToken);
        }

        public async Task<UserCredential?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.UserCredentials.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<UserCredential?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await _context.UserCredentials.FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);
        }

        public async Task<List<UserCredential>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.UserCredentials.ToListAsync(cancellationToken);
        }

        public async Task<List<UserCredential>> GetActiveCredentialsAsync(CancellationToken cancellationToken)
        {
            return await _context.UserCredentials.Where(x => !x.IsLocked).ToListAsync(cancellationToken);
        }

        public Task UpdateAsync(UserCredential userCredential, CancellationToken cancellationToken)
        {
            _context.UserCredentials.Update(userCredential);
            return Task.CompletedTask;
        }
    }
}