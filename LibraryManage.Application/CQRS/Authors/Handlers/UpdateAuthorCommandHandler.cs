using LibraryManage.Application.CQRS.Authors.Commands;
using LibraryManage.Application.Interfaces;
using LibraryManage.Domain.Entities;
using LibraryManage.Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManage.Application.CQRS.Authors.Handlers
{
    public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateAuthorCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = await _unitOfWork.AuthorRepository.GetByIdAsync(request.Id, cancellationToken);

            if (author == null)
            {
                throw new DomainException($"Author with ID '{request.Id}' not found.");
            }

            author.UpdateAuthor(
                request.FullName,
                request.BirthDate,
                request.Nationality,
                request.Bio
            );

            await _unitOfWork.AuthorRepository.UpdateAsync(author, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}