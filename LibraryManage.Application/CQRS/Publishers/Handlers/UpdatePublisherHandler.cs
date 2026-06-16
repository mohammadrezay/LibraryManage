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
    public class UpdatePublisherCommandHandler : IRequestHandler<UpdatePublisherCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePublisherCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdatePublisherCommand request, CancellationToken cancellationToken)
        {
            var publisher = await _unitOfWork.PublisherRepository.GetByIdAsync(request.Id, cancellationToken);

            if (publisher == null)
            {
                throw new DomainException("Publisher not found.");
            }

            publisher.UpdatePublisher(
                request.Name,
                request.Country,
                request.Description
            );

            await _unitOfWork.PublisherRepository.UpdateAsync(publisher, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}