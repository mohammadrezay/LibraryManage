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
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Book", t =>
            {
                t.HasCheckConstraint(
                    "CK_Book_AvailableCopies_LessOrEqual_TotalCopies",
                    "[AvailableCopies] <= [TotalCopies]"
                );
            });

            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id)
                   .ValueGeneratedNever();

            builder.Property(b => b.Title)
                   .IsRequired()
                   .HasMaxLength(300)
                   .HasColumnType("nvarchar(300)");

            builder.Property(b => b.AuthorId)
                   .IsRequired();

            builder.Property(b => b.PublisherId)
                   .IsRequired();

            builder.Property(b => b.PublicationDate)
                   .IsRequired()
                   .HasColumnType("date");

            builder.Property(b => b.TotalCopies)
                   .IsRequired();

            builder.Property(b => b.AvailableCopies)
                   .IsRequired();

            builder.Property(b => b.Language)
                   .IsRequired()
                   .HasMaxLength(50)
                   .HasColumnType("nvarchar(50)");

            builder.Property(b => b.Category)
                   .IsRequired()
                   .HasMaxLength(100)
                   .HasColumnType("nvarchar(100)");

            builder.Property(b => b.ISBN)
                   .IsRequired()
                   .HasMaxLength(20)
                   .HasColumnType("varchar(20)");

            builder.Property(b => b.Description)
                   .IsRequired()
                   .HasMaxLength(2000)
                   .HasColumnType("nvarchar(2000)");

            builder.Property(b => b.IsDeleted)
                   .IsRequired()
                   .HasDefaultValue(false);

            builder.HasOne<Author>()
                   .WithMany()
                   .HasForeignKey(b => b.AuthorId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Publisher>()
                   .WithMany()
                   .HasForeignKey(b => b.PublisherId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(b => b.Title);
            builder.HasIndex(b => b.ISBN).IsUnique();
            builder.HasIndex(b => b.AuthorId);
            builder.HasIndex(b => b.PublisherId);
            builder.HasIndex(b => new { b.AuthorId, b.Category });

            builder.HasQueryFilter(b => !b.IsDeleted);
        }
    }
}