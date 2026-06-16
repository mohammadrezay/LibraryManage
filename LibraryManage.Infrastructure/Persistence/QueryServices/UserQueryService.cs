using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManage.Application.CQRS.Users.DTOs;
using LibraryManage.Application.Interfaces;
using LibraryManage.Domain.Entities;
using LibraryManage.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace LibraryManage.Infrastructure.Persistence.QueryServices
{
    public class UserQueryService : IUserQueryService
    {
        private readonly LibraryDbContext _context;

        public UserQueryService(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserDTO>> GetAllAsync(CancellationToken cancellationToken)
        {
            var users = await _context.Users
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return users.Select(u => MapToDTO(u)).ToList();
        }

        public async Task<UserDTO?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

            return user == null ? null : MapToDTO(user);
        }

        public async Task<UserDTO?> GetByUsernameAsync(string username, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Username.Value == username, cancellationToken);

            return user == null ? null : MapToDTO(user);
        }

        public async Task<List<UserDTO>> GetActiveUsersAsync(CancellationToken cancellationToken)
        {
            var users = await _context.Users
                .AsNoTracking()
                .Where(u => !u.IsDeleted)
                .ToListAsync(cancellationToken);

            return users.Select(u => MapToDTO(u)).ToList();
        }

        private static UserDTO MapToDTO(User user)
        {
            return new UserDTO
            {
                Id = user.Id,
                FullName = user.FullName,
                Username = user.Username.Value,

                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                NationalCode = user.NationalCode,

                Province = user.Province,
                City = user.City,
                Address = user.Address,

                DateOfBirth = user.DateOfBirth,
                RegistrationDate = user.RegistrationDate
            };
        }
    }
}