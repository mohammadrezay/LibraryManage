using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace LibraryManage.Application.CQRS.Authors.Commands
{
    public class UpdateAuthorCommand : IRequest
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string Nationality { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
    }
}