using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PetProjectDraft.Domain.Common;
using PetProjectDraft.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetProjectDraft.Domain.ValueObjects;

namespace PetProjectDraft.Infrastructure.Configurations.Write
{
    public class VolunteerConfiguration : IEntityTypeConfiguration<Volunteer>
    {
        public void Configure(EntityTypeBuilder<Volunteer> builder)
        {
            builder.ToTable("volunteers");

            builder.HasKey(v => v.Id);

            builder.HasOne<User>()
                .WithOne()
                .HasForeignKey<Volunteer>(v => v.Id);

            // ComplexProperty (сложное свойство) - именно таким образом конфигурируются ValueObject-ы 
            builder.ComplexProperty(v => v.FullName, b =>
            {
                b.Property(f => f.FirstName).HasColumnName("first_name");
                b.Property(f => f.LastName).HasColumnName("last_name");
                b.Property(f => f.Patronymic).HasColumnName("patronymic").IsRequired(false);
            });

            builder.Property(v => v.Description)
                .IsRequired()
                .HasMaxLength(Constraints.LONG_TITLE_LENGTH);

            builder.Property(v => v.YearsExperience)
                .IsRequired();

            builder.Property(v => v.NumberOfPetsFoundHome)
                .IsRequired(false);

            builder.Property(v => v.DonationInfo)
                .IsRequired(false)
                .HasMaxLength(Constraints.LONG_TITLE_LENGTH);

            builder.Property(v => v.FromShelter)
                .IsRequired();


            // Конфигурация для jsonB в PostgreSql. В OwnsMany() устанавливается включение через свойства собственных типов.
            // В бд вместо таблицы будет json свойство. Здесь мы его и создаём owns many - буквально "владеет многими"
            // Отличие OwnsMany() от HasMany(), HasOne() и тп в том, что в OwnsMany() мф конкретно конфигурируем связь, которая
            // является частью этого Entity. В нашем случае SocialMedias будет являться Json - объектом, мы его так настроим,
            // а Social из строки станет полноценным ValueObjec-ом Social
            // HasConversion() - конвертируется в бд
            builder.OwnsMany(v => v.SocialMedias, navigationBuilder =>
            {
                navigationBuilder.ToJson();

                navigationBuilder.Property(s => s.Social)
                    .HasConversion(
                        s => s.Value,
                        s => Social.Create(s).Value); // HasConversion() - конвертируется в бд. В бд внесётся s.Value (то есть строка)
            });                                     // а из бд s.Value (в бд - string Social) свойство будет смаппенно в ValueObject,
                                                    // в наш Social. В бд будет string Social, а из бд будет браться Social Social 

            builder.HasMany(v => v.Photos).WithOne().IsRequired();
            builder.HasMany(v => v.Pets).WithOne().IsRequired();
        }
    }

    //public class VolunteerConfiguration : IEntityTypeConfiguration<Volunteer>
    //{
    //    public void Configure(EntityTypeBuilder<Volunteer> builder)
    //    {
    //        builder.ToTable("volunteers");

    //        builder.HasKey(v => v.Id);

    //        builder.Property(v => v.FullName).IsRequired();

    //        builder.HasOne(v => v.Photos)
    //            .WithOne();

    //        builder.HasMany(v => v.SocialMedias)
    //           .WithOne();
    //    }
    //}

}
