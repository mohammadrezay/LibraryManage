using LibraryManage.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManage.Application.Common.Security
{
    public static class PasswordPolicy
    {
        public static void Validate(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new DomainException("Password is required");
            }

            if (password.Length < 8)
            {
                throw new DomainException("Password must be at least 8 characters");
            }

            if (!password.Any(char.IsUpper))
            {
                throw new DomainException("Password must contain at least one uppercase letter");
            }

            if (!password.Any(char.IsDigit))
            {
                throw new DomainException("Password must contain at least one number");
            }
        }
    }
}