using Microsoft.AspNetCore.Authorization;
using PetProjectDraft.Api.Attributes;

namespace PetProjectDraft.Api.Authorization
{
    public class PermissionPolicyProvider : IAuthorizationPolicyProvider // позволяет зарегистрировать все политики,
                                                                         // не прописывая их по одной
    {
        public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
            var policy = new AuthorizationPolicyBuilder()
                .AddRequirements(new HasPermissionAttribute(policyName)) // policyName это и есть наша пермиссия
                .Build();

            return Task.FromResult<AuthorizationPolicy?>(policy);
        }

        public Task<AuthorizationPolicy> GetDefaultPolicyAsync() =>
            Task.FromResult(new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build());

        public Task<AuthorizationPolicy?> GetFallbackPolicyAsync() =>
            Task.FromResult<AuthorizationPolicy?>(null);
    }
}
