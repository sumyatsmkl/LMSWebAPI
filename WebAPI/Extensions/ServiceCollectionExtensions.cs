using DataAccess.Repositories.Profile;
using DataAccess.Repositories.Role;
using DataAccess.Repositories.Setup;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Services.Services;
using Services.Services.Interfaces;



namespace WebAPI.Extensions
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddServices(this IServiceCollection services, Action<DbContextOptionsBuilder> optionsAction)
        {
            services.AddDbContext<LMSDBContext>(optionsAction);

            services.AddScoped<IProfileRepository, ProfileRepository>();
            services.AddScoped<ISystemCodeValueRepository, SystemCodeValueRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();

            
            services.AddScoped<ISystemCodeValueService, SystemCodeValueService>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IRoleService, RoleService>();

            return services;
        }

        public static IServiceCollection AddAuthServices(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
            services.AddAuthorization(opt =>
                opt.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build());

            return services;
        }

        public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
            services.AddSwaggerGen(opt =>
            {
                opt.UseInlineDefinitionsForEnums();
                opt.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Health Education LMS API",
                    Version = "v1",
                    Description =
                        "To try out all the requests you have to be authorized (check the <b>Authorize</b> section)",
                });

                opt.AddSecurityDefinition("Bearer (value: SecretKey)", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the bearer scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                });

                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Id = "Bearer (value: SecretKey)",
                            Type = ReferenceType.SecurityScheme,
                        },
                    },
                    new List<string>()
                },
            });
            });

            return services;
        }
    }
}

