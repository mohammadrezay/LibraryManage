using LibraryManage.Application.Interfaces;
using LibraryManage.Infrastructure.Persistence.Context;
using LibraryManage.Infrastructure.Persistence.Repositories;
using LibraryManage.Infrastructure.Persistence.UnitOfWork;
using LibraryManage.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManage.Infrastructure.Persistence.QueryServices;
using Microsoft.Extensions.Options;

namespace LibraryManage.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<LibraryDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserCredentialRepository, UserCredentialRepository>();

            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<ILoanRepository, LoanRepository>();
            services.AddScoped<IPublisherRepository, PublisherRepository>();

            services.AddScoped<IAuthorQueryService, AuthorQueryService>();
            services.AddScoped<IBookQueryService, BookQueryService>();
            services.AddScoped<ILoanQueryService, LoanQueryService>();
            services.AddScoped<IPublisherQueryService, PublisherQueryService>();
            services.AddScoped<IUserQueryService, UserQueryService>();

            services.AddScoped<IPasswordHasher, PasswordHasher>();

            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

            return services;
        }
    }
}