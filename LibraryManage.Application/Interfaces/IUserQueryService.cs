using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManage.Application.CQRS.Users.DTOs;

namespace LibraryManage.Application.Interfaces
{
    public interface IUserQueryService
    {
        Task<List<UserDTO>> GetAllAsync(CancellationToken cancellationToken);

        Task<UserDTO?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<UserDTO?> GetByUsernameAsync(string username, CancellationToken cancellationToken);

        Task<List<UserDTO>> GetActiveUsersAsync(CancellationToken cancellationToken);
    }
}