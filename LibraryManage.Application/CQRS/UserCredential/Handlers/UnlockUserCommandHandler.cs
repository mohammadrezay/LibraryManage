using LibraryManage.Application.CQRS.UserCredential.Commands;
using LibraryManage.Application.Interfaces;
using LibraryManage.Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManage.Application.CQRS.UserCredential.Handlers
{
    public class UnlockUserCommandHandler : IRequestHandler<UnlockUserCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UnlockUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UnlockUserCommand request, CancellationToken cancellationToken)
        {
            var credential = await _unitOfWork.UserCredentialRepository.GetByUserIdAsync(request.UserId, cancellationToken);

            if (credential == null)
            {
                throw new DomainException("User credential not found");
            }

            credential.UnlockByAdmin();

            await _unitOfWork.UserCredentialRepository.UpdateAsync(credential, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}