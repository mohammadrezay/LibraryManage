using LibraryManage.Application.CQRS.Users.Commands;
using LibraryManage.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManage.Domain.ValueObjects;

namespace LibraryManage.Application.CQRS.Users.Handlers
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _jwt;

        public LoginUserCommandHandler(
            IUnitOfWork unitOfWork,
            IPasswordHasher passwordHasher,
            IJwtTokenGenerator jwt)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _jwt = jwt;
        }

        public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var username = new Username(request.Username);

            var user = await _unitOfWork.UserRepository.GetByUsernameAsync(username, cancellationToken);

            if (user == null)
            {
                throw new Exception("Invalid credentials");
            }

            var credential = await _unitOfWork.UserCredentialRepository.GetByUserIdAsync(user.Id, cancellationToken);

            if (credential == null)
            {
                throw new Exception("Invalid credentials");
            }

            var isValid = _passwordHasher.Verify(request.Password, credential.PasswordHash);

            if (!isValid)
            {
                credential.RegisterFailedLoginAttempt(5);

                await _unitOfWork.UserCredentialRepository.UpdateAsync(credential, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                throw new Exception("Invalid credentials");
            }

            credential.SuccessfulLogin();

            await _unitOfWork.UserCredentialRepository.UpdateAsync(credential, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _jwt.GenerateToken(user.Id, user.Username.Value);
        }
    }
}