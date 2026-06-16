using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManage.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManage.Infrastructure.Persistence.Configurations
{
    public class UserCredentialConfiguration : IEntityTypeConfiguration<UserCredential>
    {
        public void Configure(EntityTypeBuilder<UserCredential> builder)
        {
            builder.ToTable("UserCredential");

            builder.HasKey(uc => uc.Id);

            builder.Property(uc => uc.Id)
                   .ValueGeneratedNever();

            builder.Property(uc => uc.UserId)
                   .IsRequired();

            builder.Property(uc => uc.PasswordHash)
                   .IsRequired()
                   .HasMaxLength(500)
                   .HasColumnType("varchar(500)");

            builder.Property(uc => uc.LastPasswordChangedAt)
                   .IsRequired()
                   .HasColumnType("datetime2(0)");

            builder.Property(uc => uc.FailedLoginCount)
                   .IsRequired()
                   .HasDefaultValue(0);

            builder.Property(uc => uc.IsLocked)
                   .IsRequired()
                   .HasDefaultValue(false);

            builder.HasOne<User>()
                   .WithOne()
                   .HasForeignKey<UserCredential>(uc => uc.UserId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .HasConstraintName("FK_UserCredential_User");

            builder.HasIndex(uc => uc.UserId)
                   .IsUnique()
                   .HasDatabaseName("IX_UserCredential_UserId");

            builder.ToTable(t =>
            {
                t.HasCheckConstraint(
                    "CK_UserCredential_FailedLoginCount_NonNegative",
                    "[FailedLoginCount] >= 0 AND [FailedLoginCount] <= 10"
                );
            });
        }
    }
}