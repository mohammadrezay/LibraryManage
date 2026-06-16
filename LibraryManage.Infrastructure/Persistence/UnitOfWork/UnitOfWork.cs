using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManage.Application.Interfaces;
using LibraryManage.Infrastructure.Persistence.Context;

namespace LibraryManage.Infrastructure.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LibraryDbContext _context;

        public UnitOfWork(
            LibraryDbContext context,
            IAuthorRepository authorRepository,
            IBookRepository bookRepository,
            ILoanRepository loanRepository,
            IPublisherRepository publisherRepository,
            IUserCredentialRepository userCredentialRepository,
            IUserRepository userRepository)
        {
            _context = context;

            AuthorRepository = authorRepository;
            BookRepository = bookRepository;
            LoanRepository = loanRepository;
            PublisherRepository = publisherRepository;
            UserCredentialRepository = userCredentialRepository;
            UserRepository = userRepository;
        }

        public IAuthorRepository AuthorRepository { get; }
        public IBookRepository BookRepository { get; }
        public ILoanRepository LoanRepository { get; }
        public IPublisherRepository PublisherRepository { get; }
        public IUserCredentialRepository UserCredentialRepository { get; }
        public IUserRepository UserRepository { get; }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}