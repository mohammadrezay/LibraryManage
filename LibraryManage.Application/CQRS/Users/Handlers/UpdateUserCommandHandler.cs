using LibraryManage.Application.CQRS.Users.Commands;
using LibraryManage.Application.Interfaces;
using LibraryManage.Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManage.Application.CQRS.Users.Handlers
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(request.Id, cancellationToken);

            if (user == null)
            {
                throw new DomainException("User not found");
            }

            user.UpdateUser(
                request.FullName,
                request.Email,
                request.PhoneNumber,
                request.NationalCode,
                request.Province,
                request.City,
                request.Address,
                request.DateOfBirth
            );

            await _unitOfWork.UserRepository.UpdateAsync(user, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}