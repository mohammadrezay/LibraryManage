using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace LibraryManage.Application.CQRS.Books.Commands
{
    public class CreateBookCommand : IRequest<Guid>
    {
        public string Title { get; set; } = string.Empty;
        public Guid AuthorId { get; set; }
        public Guid PublisherId { get; set; }
        public int TotalCopies { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Language { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}