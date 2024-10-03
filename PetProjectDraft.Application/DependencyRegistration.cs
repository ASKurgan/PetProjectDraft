using Microsoft.Extensions.DependencyInjection;
using PetProjectDraft.Application.Features.VolunteerApplications.ApplyVolunteerApplication;
using PetProjectDraft.Application.Features.VolunteerApplications.ApproveVolunteerApplication;
using PetProjectDraft.Application.Features.Volunteers.PublishPet;
using PetProjectDraft.Application.Features.Volunteers.UploadPhoto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProjectDraft.Application
{
    public static class DependencyRegistration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddHandlers();
            
            return services;
        }

        private static IServiceCollection AddHandlers(this IServiceCollection services)
        {
            services.AddScoped<PublishPetHandler>();

            services.AddScoped<UploadVolunteerPhotoHandler>();

            services.AddScoped<UploadVolunteerPhotoHandler>();
          //  services.AddScoped<DeleteVolunteerPhotoHandler>();

            services.AddScoped<ApplyVolunteerApplicationHandler>();
            services.AddScoped<ApproveVolunteerApplicationHandler>();

         //   services.AddScoped<LoginHandler>();
         //   services.AddScoped<RegisterHandler>();

            return services;
        }
    }
}
