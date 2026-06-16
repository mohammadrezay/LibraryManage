using LibraryManage.Application.CQRS.Authors.Commands;
using LibraryManage.Application.Interfaces;
using LibraryManage.Domain.Entities;
using LibraryManage.Domain.Exceptions;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryManage.Application.CQRS.Authors.Handlers
{
    public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateAuthorCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new DomainException(nameof(unitOfWork));
        }

        public async Task<Guid> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            var newAuthor = new Author(
                request.FullName,
                request.BirthDate,
                request.Nationality,
                request.Bio
            );

            await _unitOfWork.AuthorRepository.AddAsync(newAuthor, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return newAuthor.Id;
        }
    }
}