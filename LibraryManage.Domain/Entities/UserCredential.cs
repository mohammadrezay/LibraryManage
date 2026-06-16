using LibraryManage.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LibraryManage.Domain.Entities
{
    public class UserCredential
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public string PasswordHash { get; private set; } = null!;
        public DateTime LastPasswordChangedAt { get; private set; }
        public int FailedLoginCount { get; private set; }
        public bool IsLocked { get; private set; }

        private UserCredential() { }

        public UserCredential(Guid userId, string passwordHash)
        {
            if(userId == Guid.Empty)
            {
                throw new DomainException("UserId is required.");
            }

            if(string.IsNullOrWhiteSpace(passwordHash))
            {
                throw new DomainException("Password hash is required.");
            }

            Id = Guid.NewGuid();
            UserId = userId;
            PasswordHash = passwordHash.Trim();
            LastPasswordChangedAt = DateTime.UtcNow;
            FailedLoginCount = 0;
            IsLocked = false;
        }

        public void ChangePasswordHash(string newPasswordHash)
        {
            EnsureNotLocked();

            if(string.IsNullOrWhiteSpace(newPasswordHash))
            {
                throw new DomainException("New password hash is required.");
            }

            PasswordHash = newPasswordHash.Trim();
            LastPasswordChangedAt = DateTime.UtcNow;
            FailedLoginCount = 0;
        }

        public void RegisterFailedLoginAttempt(int maxFailedAttempts)
        {
            if(maxFailedAttempts <= 0)
            {
                throw new DomainException("Max failed attempts must be greater than zero.");
            }

            if(IsLocked)
            {
                return;
            }

            FailedLoginCount++;

            if(FailedLoginCount >= maxFailedAttempts)
            {
                IsLocked = true;
            }
        }

        public void UnlockByAdmin()
        {
            IsLocked = false;
            FailedLoginCount = 0;
        }

        private void EnsureNotLocked()
        {
            if(IsLocked)
            {
                throw new DomainException("User credentials are locked.");
            }
        }

        public void SuccessfulLogin()
        {
            EnsureNotLocked();
            FailedLoginCount = 0;
        }
    }
}