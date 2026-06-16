using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using LibraryManage.Domain.Exceptions;

namespace LibraryManage.Domain.ValueObjects
{
    public class Username : IEquatable<Username>
    {
        private static readonly Regex UsernameRegex =
            new Regex(@"^[a-zA-Z][a-zA-Z0-9_]{3,}$", RegexOptions.Compiled);

        public string Value { get; }

        private Username() { }

        public Username(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new DomainException("Username is required.");
            }

            value = value.Trim();

            if (value.Length < 4)
            {
                throw new DomainException("Username must be at least 4 characters.");
            }

            if (!UsernameRegex.IsMatch(value))
            {
                throw new DomainException("Username must start with a letter and contain only letters, digits, or underscore.");
            }

            Value = value.ToLowerInvariant();
        }

        public override string ToString() => Value;

        public bool Equals(Username? other)
            => other is not null && Value == other.Value;

        public override bool Equals(object? obj)
            => Equals(obj as Username);

        public override int GetHashCode()
            => Value.GetHashCode();
    }
}