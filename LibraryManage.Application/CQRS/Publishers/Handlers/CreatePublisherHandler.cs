using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using LibraryManage.Application.Interfaces;
using LibraryManage.Application.CQRS.Publishers.Commands;
using LibraryManage.Domain.Entities;

namespace LibraryManage.Application.CQRS.Publishers.Handlers
{
    public class CreatePublisherCommandHandler : IRequestHandler<CreatePublisherCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreatePublisherCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreatePublisherCommand request, CancellationToken cancellationToken)
        {
            var publisher = new Publisher(
                request.Name,
                request.Country,
                request.Description
            );

            await _unitOfWork.PublisherRepository.AddAsync(publisher, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return publisher.Id;
        }
    }
}