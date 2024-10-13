using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using PetProjectDraft.Application.DataAccess;
using PetProjectDraft.Application.Features.Users;
using PetProjectDraft.Application.Features.VolunteerApplications;
using PetProjectDraft.Application.Features.Volunteers;
using PetProjectDraft.Application.Interfaces.MessageBus;
using PetProjectDraft.Application.Interfaces.Providers;
using PetProjectDraft.Infrastructure.DbContexts;
using PetProjectDraft.Infrastructure.MessageBuses;
using PetProjectDraft.Infrastructure.Options;
using PetProjectDraft.Infrastructure.Providers;
using PetProjectDraft.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProjectDraft.Infrastructure
{
    public static class DependencyRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
                                                                IConfiguration configuration)
        {
            services.AddDataStorages(configuration);
            services.AddProviders();
            services.AddRepositories();
            services.AddMessageBuses();
            services.AddChannels();
            return services;
        }


        private static IServiceCollection AddDataStorages(this IServiceCollection services,
                                                              IConfiguration configuration)
        {
            services.AddScoped<ITransaction, Transaction>();

            var connectionString = configuration.GetConnectionString("PetProject");
            
            //services.AddDbContext<PetProjectWriteDbContext>(options =>
            //{
            //    options.UseSqlServer(connectionString,
            //          b => b.MigrationsAssembly("PetProjectDraft.Infrastructure"));
            //});


             services.AddScoped<PetProjectWriteDbContext>();

            services.AddMinio(options =>
            {
                var minioOptions = configuration.GetSection(MinioOptions.Minio)
                    .Get<MinioOptions>() ?? throw new("Minio configuration not found");

                options.WithEndpoint(minioOptions.Endpoint);
                options.WithCredentials(minioOptions.AccessKey, minioOptions.SecretKey);
                options.WithSSL(false);
            });
            return services; 
        }

        private static IServiceCollection AddProviders(this IServiceCollection services)
        {
            services.AddScoped<IMinioProvider, MinioProvider>();
            services.AddScoped<IJwtProvider, JwtProvider>();
            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IVolunteersRepository, VolunteersRepository>();
            services.AddScoped<IVolunteerApplicationsRepository, VolunteerApplicationsRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            return services;
        }

        private static IServiceCollection AddMessageBuses(this IServiceCollection services)
        {
            services.AddSingleton<IMessageBus, EmailMessageBus>();
            return services;
        }

        private static IServiceCollection AddChannels(this IServiceCollection services)
        {
            services.AddSingleton<EmailMessageChannel>();
            return services;
        }

        //private static IServiceCollection AddProviders(this IServiceCollection services)
        //{
        //    services.AddScoped<IMinioProvider, MinioProvider>();
        //    services.AddScoped<IJwtProvider, JwtProvider>();
        //    services.AddSingleton<ICacheProvider, CacheProvider>();
        //    services.AddScoped<IMailProvider, MailProvider>();
        //    return services;
        //}
    }
}
