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
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.ToTable("Author");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                   .ValueGeneratedNever();

            builder.Property(a => a.FullName)
                   .IsRequired()
                   .HasMaxLength(200)
                   .HasColumnType("nvarchar(200)");

            builder.Property(a => a.BirthDate)
                   .IsRequired()
                   .HasColumnType("date");

            builder.Property(a => a.Nationality)
                   .IsRequired()
                   .HasMaxLength(100)
                   .HasColumnType("nvarchar(100)");

            builder.Property(a => a.Bio)
                   .IsRequired()
                   .HasMaxLength(1000)
                   .HasColumnType("nvarchar(1000)");

            builder.Property(a => a.IsDeleted)
                   .IsRequired()
                   .HasDefaultValue(false);

            builder.HasIndex(a => a.FullName);

            builder.HasQueryFilter(a => !a.IsDeleted);
        }
    }
}