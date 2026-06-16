using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LibraryManage.Domain.Exceptions;

namespace LibraryManage.Domain.Entities
{
    public class Author
    {
        public Guid Id { get; private set; }
        public string FullName { get; private set; }
        public DateTime BirthDate { get; private set; }
        public string Nationality { get; private set; }
        public string Bio { get; private set; }
        public bool IsDeleted { get; private set; }

        private Author() { }

        public Author(string fullName, DateTime birthDate, string nationality, string bio)
        {
            if(string.IsNullOrWhiteSpace(fullName))
            {
                throw new DomainException("FullName is required");
            }

            if(birthDate == default(DateTime))
            {
                throw new DomainException("Birth Date is required");
            }

            if(string.IsNullOrWhiteSpace(nationality))
            {
                throw new DomainException("Nationality is required");
            }

            if(string.IsNullOrWhiteSpace(bio))
            {
                throw new DomainException("Bio is required");
            }

            Id = Guid.NewGuid();
            FullName = fullName.Trim();
            BirthDate = birthDate;
            Nationality = nationality.Trim();
            Bio = bio.Trim();
            IsDeleted = false;
        }

        public void UpdateAuthor(string fullName, DateTime birthDate, string nationality, string bio)
        {
            EnsureNotDeleted();

            if (!string.IsNullOrWhiteSpace(fullName))
            {
                FullName = fullName.Trim();
            }

            if(birthDate != default(DateTime))
            {
                BirthDate = birthDate;
            }

            if(!string.IsNullOrWhiteSpace(nationality))
            {
                Nationality = nationality.Trim();
            }

            if(!string.IsNullOrWhiteSpace(bio))
            {
                Bio = bio.Trim();
            }
        }

        public void Delete()
        {
            EnsureNotDeleted();
            IsDeleted = true;
        }

        private void EnsureNotDeleted()
        {
            if(IsDeleted)
            {
                throw new DomainException("Author is deleted.");
            }
        }
    }
}