using PetProjectDraft.Api.Requests.PublishPet;
using PetProjectDraft.Application.Features.Volunteers.PublishPet;
using PetProjectDraft.Application.Interfaces.Providers;
using PetProjectDraft.Infrastructure.Providers;

namespace PetProjectDraft.Api
{
    public static class DependencyRegistration
    {
        public static IServiceCollection AddApi(this IServiceCollection services)
        {
            services.AddHandlers();   
            return services;
        }

        private static IServiceCollection AddHandlers(this IServiceCollection services)
        {
            services.AddScoped<PublishPetHandlerApi>();
            return services;
        }
    }
}
