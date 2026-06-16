using LibraryManage.Application.CQRS.UserCredential.Commands;
using LibraryManage.Application.Interfaces;
using LibraryManage.Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManage.Application.Common.Security;

namespace LibraryManage.Application.CQRS.UserCredential.Handlers
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;

        public ChangePasswordCommandHandler(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
        }

        public async Task<Unit> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var credential = await _unitOfWork.UserCredentialRepository.GetByUserIdAsync(request.UserId, cancellationToken);

            if (credential == null)
            {
                throw new DomainException("User credential not found");
            }

            PasswordPolicy.Validate(request.NewPassword);

            var newHash = _passwordHasher.Hash(request.NewPassword);

            credential.ChangePasswordHash(newHash);

            await _unitOfWork.UserCredentialRepository.UpdateAsync(credential, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}