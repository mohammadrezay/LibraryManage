using System;
using System.Linq;
using LibraryManage.Domain.Enums;
using LibraryManage.Domain.Exceptions;

namespace LibraryManage.Domain.Entities
{
    public class Book
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public Guid AuthorId { get; private set; }
        public Guid PublisherId { get; private set; }
        public DateTime PublicationDate { get; private set; }
        public int TotalCopies { get; private set; }
        public int AvailableCopies { get; private set; }
        public string Language { get; private set; }
        public string Category { get; private set; }
        public string ISBN { get; private set; }
        public string Description { get; private set; }
        public bool IsDeleted { get; private set; }

        private Book() { }

        public Book(string title, Guid authorId, int totalCopies, Guid publisherId,
                    DateTime publicationDate, string language, string category, string iSBN,
                    string description)
        {
            if (totalCopies < 0)
            {
                throw new DomainException("TotalCopies must be >= 0");
            }

            if (string.IsNullOrWhiteSpace(title))
            {
                throw new DomainException("Title is required");
            }

            if(authorId == Guid.Empty)
            {
                throw new DomainException("AuthorId is required.");
            }

            if (publisherId == Guid.Empty)
            {
                throw new DomainException("PublisherId is required.");
            }

            if (publicationDate == default(DateTime))
            {
                throw new DomainException("Publication Date is required");
            }

            if (string.IsNullOrWhiteSpace(language))
            {
                throw new DomainException("Language is required");
            }

            if (string.IsNullOrWhiteSpace(category))
            {
                throw new DomainException("Category is required");
            }

            if (string.IsNullOrWhiteSpace(iSBN))
            {
                throw new DomainException("ISBN is required");
            }

            if (string.IsNullOrWhiteSpace(description))
            {
                throw new DomainException("Description is required");
            }

            Id = Guid.NewGuid();
            Title = title.Trim();
            AuthorId = authorId;
            PublisherId = publisherId;
            PublicationDate = publicationDate;
            TotalCopies = totalCopies;
            AvailableCopies = totalCopies;
            Language = language.Trim();
            Category = category.Trim();
            ISBN = iSBN.Trim();
            Description = description.Trim();
            IsDeleted = false;
        }

        public void IncreaseCopies(int amount)
        {
            if (amount <= 0)
            {
                throw new DomainException("Amount must be > 0");
            }

            TotalCopies += amount;
            AvailableCopies += amount;
        }

        public void DecreaseCopies(int amount)
        {
            if(amount <= 0)
            {
                throw new DomainException("Amount must be > 0");
            }

            var loanedCopies = TotalCopies - AvailableCopies;

            if(TotalCopies - amount < loanedCopies)
            {
                throw new DomainException("Cannot remove copies that are currently loaned.");
            }

            TotalCopies -= amount;

            if(AvailableCopies >= amount)
            {
                AvailableCopies -= amount;
            }
            else
            {
                AvailableCopies = 0;
            }
        }

        public void UpdateBook(string title, Guid authorId, Guid publisherId, DateTime publicationDate,
                                int totalCopies, string language, string category, string iSBN,
                                string description)
        {
            EnsureNotDeleted();

            if (!string.IsNullOrWhiteSpace(title))
            {
                Title = title.Trim();
            }

            if(authorId != Guid.Empty)
            {
                AuthorId = authorId;
            }

            if (publisherId != Guid.Empty)
            {
                PublisherId = publisherId;
            }

            if (publicationDate != default(DateTime))
            {
                PublicationDate = publicationDate;
            }

            if(totalCopies >= 0)
            {
                var loanedCopies = TotalCopies - AvailableCopies;

                if(totalCopies < loanedCopies)
                {
                    throw new DomainException("TotalCopies cannot be less than loaned copies.");
                }

                TotalCopies = totalCopies;
                AvailableCopies = totalCopies - loanedCopies;
            }

            if (!string.IsNullOrWhiteSpace(language))
            {
                Language = language.Trim();
            }

            if (!string.IsNullOrWhiteSpace(category))
            {
                Category = category.Trim();
            }

            if (!string.IsNullOrWhiteSpace(iSBN))
            {
                ISBN = iSBN.Trim();
            }

            if (!string.IsNullOrWhiteSpace(description))
            {
                Description = description.Trim();
            }
        }

        public void Delete()
        {
            EnsureNotDeleted();

            if(TotalCopies != AvailableCopies)
            {
                throw new DomainException("Cannot delete a book with active loans.");
            }

            IsDeleted = true;
        }

        private void EnsureNotDeleted()
        {
            if (IsDeleted)
            {
                throw new DomainException("Book is deleted.");
            }
        }
    }
}