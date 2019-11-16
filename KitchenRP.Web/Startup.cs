using System.Text;
using KitchenRP.DataAccess;
using KitchenRP.Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace KitchenRP.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options =>
                options.Filters.Add(new HttpExceptionFilter()));

            services.AddDbContext<KitchenRpContext>(cfg =>
            {
                cfg.UseNpgsql(Configuration.GetConnectionString("default"),
                    b => b
                        .MigrationsAssembly("KitchenRP.Web")
                        .UseNodaTime());
            });

            services.AddKitchenRpDataAccessService(null);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "KitchenRP-Api",
                    Version = "v1"
                });
                var tokenScheme = new OpenApiSecurityScheme
                {
                    Description = "JWT authorization header. Example: \"Authorization: Bearer {token}\"",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    BearerFormat = "Authorization: Bearer $token"
                };

                c.AddSecurityDefinition("Bearer", tokenScheme);

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {tokenScheme, new string[] { }}
                });
            });

            var accessKey = Encoding.ASCII.GetBytes(Configuration["Jwt:AccessSecret"]);
            var refreshKey = Encoding.ASCII.GetBytes(Configuration["Jwt:RefreshSecret"]);

            services.AddKitchenRpDomainServices(c =>
            {
                c.LdapConfiguration(lc =>
                {
                    lc.Host = Configuration["Ldap:Host"];
                    lc.Port = ushort.Parse(Configuration["Ldap:Port"]);
                    lc.SearchBase = Configuration["Ldap:SearchBase"];
                    lc.UserSearch = Configuration["Ldap:UserSearch"];
                });
                c.JwtConfiguration(jwt =>
                {
                    jwt.AccessSecret = accessKey;
                    jwt.RefreshSecret = refreshKey;
                    jwt.AccessTimeout = int.Parse(Configuration["Jwt:AccessTimeout"]);
                    jwt.RefreshTimeout = int.Parse(Configuration["Jwt:RefreshTimeout"]);
                });
            });

            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(accessKey),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler(env.IsDevelopment() ? "/error-local-development" : "/error");

            app.UseHttpsRedirection();

            app.UseRouting();


            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "KitchenRP-API"));
        }
    }
}
