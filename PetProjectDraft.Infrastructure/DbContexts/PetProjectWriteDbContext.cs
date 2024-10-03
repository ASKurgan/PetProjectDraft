using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PetProjectDraft.Application.DataAccess;
using PetProjectDraft.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace PetProjectDraft.Infrastructure.DbContexts
{
    public class PetProjectWriteDbContext : DbContext //, ITransaction
    {
        private readonly IConfiguration _configuration;


        public PetProjectWriteDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public DbSet<Pet> Pets => Set<Pet>();
        public DbSet<VolunteerApplication> VolunteersApplications => Set<VolunteerApplication>();
        public DbSet<Volunteer> Volunteers => Set<Volunteer>();
        public DbSet<User> Users => Set<User>();
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("PetProject"),
                b => b.MigrationsAssembly("PetProjectDraft.Infrastructure"));

            optionsBuilder.UseSnakeCaseNamingConvention();// Переводит в нижний регистр с подчёркиваниями.
                                                          // Так будет в бд

            optionsBuilder.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(PetProjectWriteDbContext).Assembly,
                type => type.FullName?.Contains("Configurations.Write") ?? false);
        }
    }
}
