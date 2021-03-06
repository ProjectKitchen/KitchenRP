using System;
using System.ComponentModel.Design;
using KitchenRP.DataAccess.Repositories;
using KitchenRP.DataAccess.Repositories.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace KitchenRP.DataAccess
{
    public static class DataAccessServicesExtension
    {
        public static IServiceCollection AddKitchenRpDataAccessService(this IServiceCollection services,
            Action<DbContextOptionsBuilder> dbContextOptions)
        {
            //services.AddDbContext<KitchenRpContext>(dbContextOptions);
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRolesRepository, RolesRepository>();
            services.AddScoped<IResourceRepository, ResourceRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IReservationRepository, ReservationRepository>();
            services.AddScoped<IReservationStatusRepository, ReservationStatusRepository>();
            return services;
        }
    }
}