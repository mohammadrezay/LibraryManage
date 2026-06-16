using LibraryManage.Application.CQRS.Authors.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManage.Application.Interfaces
{
    public interface IAuthorQueryService
    {
        Task<List<AuthorDTO>> GetAllAsync(CancellationToken cancellationToken);
        Task<AuthorDTO?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}