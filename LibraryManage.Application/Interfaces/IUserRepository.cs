using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManage.Domain.Entities;
using LibraryManage.Domain.ValueObjects;

namespace LibraryManage.Application.Interfaces
{
    public interface IUserRepository
    {
        Task AddAsync(User user, CancellationToken cancellationToken);

        Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);

        Task<User?> GetByNationalCodeAsync(string nationalCode, CancellationToken cancellationToken);

        Task<User?> GetByUsernameAsync(Username username, CancellationToken cancellationToken);

        Task<List<User>> GetAllAsync(CancellationToken cancellationToken);

        Task<List<User>> GetActiveUsersAsync(CancellationToken cancellationToken);

        Task UpdateAsync(User user, CancellationToken cancellationToken);
    }
}