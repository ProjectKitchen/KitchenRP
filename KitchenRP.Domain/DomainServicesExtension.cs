using System;
using KitchenRP.Domain.Services;
using KitchenRP.Domain.Services.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace KitchenRP.Domain
{
    /// <summary>
    ///     Service extension for adding and configuring all services of this project at once
    /// </summary>
    public static class DomainServicesExtension
    {
        public static IServiceCollection AddKitchenRpDomainServices(this IServiceCollection services,
            Action<KitchenRpServiceOptions> configurer)
        {
            var options = new KitchenRpServiceOptions();
            configurer.Invoke(options);
            services.AddScoped(options.AuthService);
            services.AddScoped(options.JwtService);
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthorizationService, KitchenRpAuthorizationService>();
            return services;
        }
    }
}