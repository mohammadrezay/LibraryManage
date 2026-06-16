using LibraryManage.Application.CQRS.Users.Commands;
using LibraryManage.Application.Interfaces;
using LibraryManage.Domain.Entities;
using LibraryManage.Domain.Exceptions;
using LibraryManage.Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManage.Application.Common.Security;

namespace LibraryManage.Application.CQRS.Users.Handlers
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;

        public RegisterUserCommandHandler(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
        }

        public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var username = new Username(request.Username);

            var existingUser = await _unitOfWork.UserRepository.GetByUsernameAsync(username, cancellationToken);

            if (existingUser != null)
            {
                throw new DomainException("Username already exists");
            }

            PasswordPolicy.Validate(request.Password);

            var user = new User(
                request.FullName,
                username,
                request.Email,
                request.PhoneNumber,
                request.NationalCode,
                request.Province,
                request.City,
                request.Address,
                request.DateOfBirth,
                DateTime.UtcNow
            );

            await _unitOfWork.UserRepository.AddAsync(user, cancellationToken);

            var passwordHash = _passwordHasher.Hash(request.Password);

            var credential = new LibraryManage.Domain.Entities.UserCredential(user.Id, passwordHash);

            await _unitOfWork.UserCredentialRepository.AddAsync(credential, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}