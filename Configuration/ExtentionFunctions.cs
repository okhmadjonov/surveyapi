using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using surveyapi.Entities;
using surveyapi.Repositories;
using surveyapi.Services;
using System.Text;

namespace surveyapi.Configuration;


public static class ExtentionFunctions
{
    public static IServiceCollection AddServiceFunctionsConfiguration(
        this IServiceCollection services
    )
    {

        services.AddScoped<IGenericRepository<User>, GenericRepository<User>>();
        services.AddScoped<IGenericRepository<Choice>, GenericRepository<Choice>>();
        services.AddScoped<IGenericRepository<Question>, GenericRepository<Question>>();
        services.AddScoped<IGenericRepository<Survey>, GenericRepository<Survey>>();
        services.AddScoped<IGenericRepository<Role>, GenericRepository<Role>>();
        services.AddScoped<IGenericRepository<UserRole>, GenericRepository<UserRole>>();
        services.AddScoped<IGenericRepository<UserSurvey>, GenericRepository<UserSurvey>>();
        services.AddScoped<IGenericRepository<Person>, GenericRepository<Person>>();
        return services;
    }


    public static IServiceCollection AddErrorFilter(this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });
    
        return services;
    }

    //JWT bearer extention function
    public static void AddSwaggerService(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var jwtSettings = configuration.GetSection("Jwt");
        services.AddSwaggerGen(p =>
        {
            p.ResolveConflictingActions(ad => ad.First());
            p.AddSecurityDefinition(
                "Bearer",
                new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                }
            );
            p.AddSecurityRequirement(
                new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference = new OpenApiReference()
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                }
            );
        });

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings["SecretKey"])
                    )
                };
            });
    }
}