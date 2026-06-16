using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using LibraryManage.Application.Interfaces;
using LibraryManage.Application.CQRS.Publishers.Commands;
using LibraryManage.Domain.Exceptions;

namespace LibraryManage.Application.CQRS.Publishers.Handlers
{
    public class DeletePublisherCommandHandler : IRequestHandler<DeletePublisherCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeletePublisherCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeletePublisherCommand request, CancellationToken cancellationToken)
        {
            var publisher = await _unitOfWork.PublisherRepository
                .GetByIdAsync(request.Id, cancellationToken);

            if (publisher == null)
            {
                throw new DomainException("Publisher not found.");
            }

            publisher.Delete();

            await _unitOfWork.PublisherRepository.UpdateAsync(publisher, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}