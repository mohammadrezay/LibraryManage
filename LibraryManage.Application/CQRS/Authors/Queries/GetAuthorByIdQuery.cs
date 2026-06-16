using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using LibraryManage.Application.CQRS.Authors.DTOs;

namespace LibraryManage.Application.CQRS.Authors.Queries
{
    public class GetAuthorByIdQuery : IRequest<AuthorDTO?>
    {
        public Guid Id { get; set; }
    }
}