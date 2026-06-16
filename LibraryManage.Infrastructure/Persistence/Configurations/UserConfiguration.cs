using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManage.Domain.Entities;
using LibraryManage.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManage.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                   .ValueGeneratedNever();

            builder.Property(u => u.FullName)
                   .IsRequired()
                   .HasMaxLength(200)
                   .HasColumnType("nvarchar(200)");

            builder.OwnsOne(u => u.Username, username =>
            {
                username.Property(x => x.Value)
                        .HasColumnName("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                username.HasIndex(x => x.Value)
                        .IsUnique()
                        .HasDatabaseName("IX_User_Username");
            });

            builder.Property(u => u.Email)
                   .IsRequired()
                   .HasMaxLength(150)
                   .HasColumnType("varchar(150)");

            builder.Property(u => u.PhoneNumber)
                   .IsRequired()
                   .HasMaxLength(11)
                   .HasColumnType("varchar(11)");

            builder.Property(u => u.NationalCode)
                   .IsRequired()
                   .HasMaxLength(10)
                   .HasColumnType("varchar(10)");

            builder.Property(u => u.Province)
                   .IsRequired()
                   .HasMaxLength(100)
                   .HasColumnType("nvarchar(100)");

            builder.Property(u => u.City)
                   .IsRequired()
                   .HasMaxLength(100)
                   .HasColumnType("nvarchar(100)");

            builder.Property(u => u.Address)
                   .IsRequired()
                   .HasMaxLength(500)
                   .HasColumnType("nvarchar(500)");

            builder.Property(u => u.DateOfBirth)
                   .IsRequired()
                   .HasColumnType("date");

            builder.Property(u => u.RegistrationDate)
                   .IsRequired()
                   .HasColumnType("datetime2(0)");

            builder.Property(u => u.IsDeleted)
                   .IsRequired()
                   .HasDefaultValue(false);

            builder.HasIndex(u => u.Email)
                   .IsUnique();

            builder.HasIndex(u => u.PhoneNumber)
                   .IsUnique();

            builder.HasIndex(u => u.NationalCode)
                   .IsUnique();

            builder.HasQueryFilter(u => !u.IsDeleted);

            builder.ToTable(t =>
            {
                t.HasCheckConstraint(
                    "CK_User_PhoneNumber_Format",
                    "LEN([PhoneNumber]) = 11 AND [PhoneNumber] LIKE '0%' AND [PhoneNumber] NOT LIKE '%[^0-9]%'"
                );

                t.HasCheckConstraint(
                    "CK_User_NationalCode_Format",
                    "LEN([NationalCode]) = 10 AND [NationalCode] NOT LIKE '%[^0-9]%'"
                );

                t.HasCheckConstraint(
                    "CK_User_Email_Format",
                    "[Email] LIKE '%@%.%' AND [Email] NOT LIKE '% %'"
                );
            });
        }
    }
}