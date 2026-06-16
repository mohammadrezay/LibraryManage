using LibraryManage.Domain.Entities;
using LibraryManage.Domain.ValueObjects;
using LibraryManage.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace LibraryManage.Infrastructure.Persistence.Seed
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(LibraryDbContext context)
        {
            if (await context.Users.AnyAsync())
            {
                return;
            }

            var author = new Author(
                "J.K. Rowling",
                new DateTime(1965, 7, 31),
                "British",
                "Author of Harry Potter"
            );

            var publisher = new Publisher(
                "Bloomsbury",
                "UK",
                "Famous publisher"
            );

            var book = new Book(
                "Harry Potter",
                author.Id,
                10, // totalCopies
                publisher.Id,
                new DateTime(2000, 1, 1),
                "English",
                "Fantasy",
                "123456789",
                "Magic story"
            );

            var user = new User(
                "Ali Ahmadi",
                new Username("ali123"),
                "ali@test.com",
                "09123456789",
                "0480979421",
                "Tehran",
                "Tehran",
                "Some Address",
                new DateTime(1995, 1, 1),
                DateTime.UtcNow
            );

            var userCredential = new UserCredential(
                user.Id,
                "hashed-password"
            );

            var loan = new Loan(
                user.Id,
                book.Id,
                DateTime.UtcNow
            );

            await context.Authors.AddAsync(author);
            await context.Publishers.AddAsync(publisher);
            await context.Books.AddAsync(book);
            await context.Users.AddAsync(user);
            await context.UserCredentials.AddAsync(userCredential);
            await context.Loans.AddAsync(loan);

            await context.SaveChangesAsync();
        }
    }
}