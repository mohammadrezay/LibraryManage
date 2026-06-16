using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManage.Domain.Entities;

namespace LibraryManage.Application.Interfaces
{
    public interface IUserCredentialRepository
    {
        Task AddAsync(UserCredential userCredential, CancellationToken cancellationToken);

        Task<UserCredential?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<UserCredential?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken);

        Task<List<UserCredential>> GetAllAsync(CancellationToken cancellationToken);

        Task<List<UserCredential>> GetActiveCredentialsAsync(CancellationToken cancellationToken);

        Task UpdateAsync(UserCredential userCredential, CancellationToken cancellationToken);
    }
}