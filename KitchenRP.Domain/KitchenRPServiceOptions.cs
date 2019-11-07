using System;
using KitchenRP.DataAccess.Repositories;
using KitchenRP.Domain.Services;
using KitchenRP.Domain.Services.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace KitchenRP.Domain
{
    /// <summary>
    ///     Options for configuring services of this project
    /// </summary>
    public class KitchenRpServiceOptions
    {
        private Func<IServiceProvider, IAuthenticationService>? _authService;
        private Func<IServiceProvider, IJwtService>? _jwtService;

        //this function is generated dynamically based on the configuration and is used as a factory for services of this type
        internal Func<IServiceProvider, IAuthenticationService> AuthService =>
            _authService ?? throw new ServiceNotInitializedException();

        internal Func<IServiceProvider, IJwtService> JwtService =>
            _jwtService ?? throw new ServiceNotInitializedException();

        public void LdapConfiguration(Action<LdapConfiguration> configuration)
        {
            _authService = _ =>
            {
                var cfg = new LdapConfiguration();
                configuration.Invoke(cfg);
                return new LdapAuthService(cfg.Host!, cfg.Port ?? 0, cfg.SearchBase!, cfg.UserSearch!);
            };
        }

        public void JwtConfiguration(Action<JwtConfiguration> configuration)
        {
            _jwtService = services =>
            {
                var cfg = new JwtConfiguration();
                configuration.Invoke(cfg);
                var refreshTokens = services.GetService<IRefreshTokenRepository>();
                return new JwtService(refreshTokens, cfg.AccessSecret!, cfg.AccessTimeout!, cfg.RefreshSecret!,
                    cfg.RefreshTimeout!);
            };
        }
    }

    public class LdapConfiguration
    {
        public string? Host { get; set; }
        public ushort? Port { get; set; }
        public string? SearchBase { get; set; }
        public string? UserSearch { get; set; }
    }

    public class JwtConfiguration
    {
        public byte[]? AccessSecret { get; set; }
        public byte[]? RefreshSecret { get; set; }
        public int AccessTimeout { get; set; } = -1;
        public int RefreshTimeout { get; set; } = -1;
    }

    public class ServiceNotInitializedException : Exception
    {
    }
}