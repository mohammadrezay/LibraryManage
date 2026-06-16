using LibraryManage.Domain.Exceptions;
using LibraryManage.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManage.Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public string FullName { get; private set; }
        public Username Username { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }
        public string NationalCode { get; private set; }
        public string Province { get; private set; }
        public string City { get; private set; }
        public string Address { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public DateTime RegistrationDate { get; private set; }

        public bool IsDeleted { get; private set; }

        private User() { }

        public User(string fullName, Username username, string email, string phoneNumber, string nationalCode,
                    string province, string city, string address, DateTime dateOfBirth,
                    DateTime registrationDate)
        {
            if(string.IsNullOrWhiteSpace(fullName))
            {
                throw new DomainException(nameof(fullName));
            }

            if(username == null)
            {
                throw new DomainException("Username is required");
            }

            ValidateEmail(email);
            ValidatePhoneNumber(phoneNumber);
            ValidateNationalCode(nationalCode);

            if(string.IsNullOrWhiteSpace(province))
            {
                throw new DomainException("Province is required");
            }

            if(string.IsNullOrWhiteSpace(city))
            {
                throw new DomainException("City is required");
            }

            if(string.IsNullOrWhiteSpace(address))
            {
                throw new DomainException("Address is required");
            }

            if(dateOfBirth == default(DateTime))
            {
                throw new DomainException("DateOfBirth is required");
            }

            if(dateOfBirth.Date > DateTime.UtcNow.Date)
            {
                throw new DomainException("DateOfBirth cannot be in the future.");
            }

            if(registrationDate == default(DateTime))
            {
                throw new DomainException("RegistrationDate is required");
            }

            Id = Guid.NewGuid();
            FullName = fullName.Trim();
            Username = username;
            Email = email.Trim();
            PhoneNumber = phoneNumber.Trim();
            NationalCode = nationalCode.Trim();

            Province = province.Trim();
            City = city.Trim();
            Address = address.Trim();

            DateOfBirth = dateOfBirth.Date;
            RegistrationDate = registrationDate;
            IsDeleted = false;
        }

        public void UpdateUser(string fullName, string email, string phoneNumber, string nationalCode,
                               string province, string city, string address, DateTime dateOfBirth)
        {
            EnsureNotDeleted();

            if (!string.IsNullOrWhiteSpace(fullName))
            {
                FullName = fullName.Trim();
            }

            if(!string.IsNullOrWhiteSpace(email))
            {
                ValidateEmail(email);
                Email = email.Trim();
            }

            if(!string.IsNullOrWhiteSpace(phoneNumber))
            {
                ValidatePhoneNumber(phoneNumber);
                PhoneNumber = phoneNumber.Trim();
            }

            if(!string.IsNullOrWhiteSpace(nationalCode))
            {
                ValidateNationalCode(nationalCode);
                NationalCode = nationalCode.Trim();
            }

            if(!string.IsNullOrWhiteSpace(province))
            {
                Province = province.Trim();
            }

            if(!string.IsNullOrWhiteSpace(city))
            {
                City = city.Trim();
            }

            if(!string.IsNullOrWhiteSpace(address))
            {
                Address = address.Trim();
            }

            if(dateOfBirth != default(DateTime))
            {
                if(dateOfBirth.Date > DateTime.UtcNow.Date)
                {
                    throw new DomainException("DateOfBirth cannot be in the future.");
                }

                DateOfBirth = dateOfBirth.Date;
            }
        }

        public void Delete()
        {
            EnsureNotDeleted();
            IsDeleted = true;
        }

        private static void ValidateEmail(string email)
        {
            if(string.IsNullOrWhiteSpace(email))
            {
                throw new DomainException("Email is required");
            }

            try
            {
                var address = new MailAddress(email);

                if(!string.Equals(address.Address, email, StringComparison.Ordinal))
                {
                    throw new DomainException("Email format is invalid.");
                }
            }
            catch
            {
                throw new DomainException("Email format is invalid.");
            }
        }

        private static void ValidatePhoneNumber(string phoneNumber)
        {
            if(string.IsNullOrWhiteSpace(phoneNumber))
            {
                throw new DomainException("PhoneNumber is required");
            }

            if(phoneNumber.Length != 11)
            {
                throw new DomainException("PhoneNumber length must be exactly 11.");
            }

            if(!phoneNumber.StartsWith("0"))
            {
                throw new DomainException("PhoneNumber must start with 0.");
            }

            if(!phoneNumber.All(char.IsDigit))
            {
                throw new DomainException("PhoneNumber must contain only digits.");
            }
        }

        private static void ValidateNationalCode(string nationalCode)
        {
            if(string.IsNullOrWhiteSpace(nationalCode))
            {
                throw new DomainException("NationalCode is required");
            }

            if(nationalCode.Length != 10)
            {
                throw new DomainException("NationalCode length must be exactly 10.");
            }

            if(!nationalCode.All(char.IsDigit))
            {
                throw new DomainException("NationalCode must contain only digits.");
            }

            if(new string(nationalCode[0], 10) == nationalCode)
            {
                throw new DomainException("NationalCode is invalid.");
            }

            int sum = 0;
            for(int i = 0; i < 9; i++)
            {
                sum += (nationalCode[i] - '0') * (10 - i);
            }

            int remainder = sum % 11;
            int checkDigit = nationalCode[9] - '0';

            bool isValid =
                (remainder < 2 && checkDigit == remainder) ||
                (remainder >= 2 && checkDigit == 11 - remainder);

            if(!isValid)
            {
                throw new DomainException("NationalCode is invalid.");
            }
        }

        private void EnsureNotDeleted()
        {
            if(IsDeleted)
            {
                throw new DomainException("User is deleted.");
            }
        }
    }
}