using LibraryManage.Domain.Entities;
using LibraryManage.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManage.Infrastructure.Persistence.Configurations
{
    public class LoanConfiguration : IEntityTypeConfiguration<Loan>
    {
        public void Configure(EntityTypeBuilder<Loan> builder)
        {
            builder.ToTable("Loan");

            builder.HasKey(l => l.Id);

            builder.Property(l => l.Id)
                   .ValueGeneratedNever();

            builder.Property(l => l.UserId).IsRequired();
            builder.Property(l => l.BookId).IsRequired();

            builder.Property(l => l.LoanDate)
                   .IsRequired()
                   .HasColumnType("date");

            builder.Property(l => l.DueDate)
                   .IsRequired()
                   .HasColumnType("date");

            builder.Property(l => l.ReturnDate)
                   .HasColumnType("date");

            builder.Property(l => l.Status)
                   .IsRequired()
                   .HasConversion<int>();

            builder.Property(l => l.TotalFine)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)")
                   .HasDefaultValue(0m);

            builder.OwnsMany(l => l.PaymentHistories, ph =>
            {
                ph.ToTable("PaymentHistory");

                ph.WithOwner().HasForeignKey("LoanId");
                ph.HasKey(p => p.Id);

                ph.Property(p => p.Id)
                  .ValueGeneratedNever();

                ph.Property<Guid>("LoanId");

                ph.Property(p => p.Amount)
                  .IsRequired()
                  .HasColumnType("decimal(18,2)");

                ph.Property(p => p.PaymentDate)
                  .IsRequired()
                  .HasColumnType("date");

                ph.HasIndex("LoanId");
            });

            builder.Metadata
                   .FindNavigation(nameof(Loan.PaymentHistories))!
                   .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasOne<User>()
                   .WithMany()
                   .HasForeignKey(l => l.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Book>()
                   .WithMany()
                   .HasForeignKey(l => l.BookId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(l => l.UserId);
            builder.HasIndex(l => l.BookId);
            builder.HasIndex(l => l.Status);
            builder.HasIndex(l => l.DueDate);
            builder.HasIndex(l => new { l.UserId, l.Status });
            builder.HasIndex(l => new { l.BookId, l.Status });
            builder.HasIndex(l => new { l.Status, l.DueDate });

            builder.ToTable(t =>
            {
                t.HasCheckConstraint(
                    "CK_Loan_ReturnDate_GreaterOrEqual_LoanDate",
                    "([ReturnDate] IS NULL OR [ReturnDate] >= [LoanDate]) AND [DueDate] >= [LoanDate]"
                );
            });
        }
    }
}