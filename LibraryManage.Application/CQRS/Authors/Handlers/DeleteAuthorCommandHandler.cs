using System;
using System.Threading;
using System.Threading.Tasks;
using LibraryManage.Application.CQRS.Authors.Commands;
using LibraryManage.Application.Interfaces;
using LibraryManage.Domain.Entities;
using LibraryManage.Domain.Exceptions;
using MediatR;

namespace LibraryManage.Application.CQRS.Authors.Handlers
{
    public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAuthorCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = await _unitOfWork.AuthorRepository.GetByIdAsync(request.Id, cancellationToken);

            if (author == null)
            {
                throw new DomainException($"Author with ID '{request.Id}' not found.");
            }

            author.Delete();

            await _unitOfWork.AuthorRepository.UpdateAsync(author, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}