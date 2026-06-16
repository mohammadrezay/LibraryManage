using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManage.Domain.Exceptions;

namespace LibraryManage.Domain.Entities
{
    public class Publisher
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Country { get; private set; }
        public string? Description { get; private set; }
        public bool IsDeleted { get; private set; }

        private Publisher() { }

        public Publisher(string name, string country, string? description)
        {
            Id = Guid.NewGuid();
            Name = ValidateName(name);
            Country = ValidateCountry(country);
            Description = NormalizeDescription(description);
        }

        public void UpdatePublisher(string? name, string? country, string? description)
        {
            EnsureNotDeleted();

            if (name != null)
            {
                Name = ValidateName(name);
            }

            if (country != null)
            {
                Country = ValidateCountry(country);
            }

            if (description != null)
            {
                Description = NormalizeDescription(description);
            }
        }

        public void Delete()
        {
            EnsureNotDeleted();
            IsDeleted = true;
        }

        private static string ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new DomainException("PUBLISHER_NAME_REQUIRED", "Name is required");
            }

            return name.Trim();
        }

        private static string ValidateCountry(string country)
        {
            if (string.IsNullOrWhiteSpace(country))
            {
                throw new DomainException("PUBLISHER_COUNTRY_REQUIRED", "Country is required");
            }

            return country.Trim();
        }

        private static string? NormalizeDescription(string? description)
        {
            return string.IsNullOrWhiteSpace(description)
                ? null
                : description.Trim();
        }

        private void EnsureNotDeleted()
        {
            if (IsDeleted)
            {
                throw new DomainException("PUBLISHER_DELETED", "Publisher is deleted.");
            }
        }
    }
}