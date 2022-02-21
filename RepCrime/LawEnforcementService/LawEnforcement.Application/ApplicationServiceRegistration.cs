using FluentValidation;
using LawEnforcement.Application.Behaviours;
using LawEnforcement.Domain.DTO;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace LawEnforcement.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<UnhandledExceptionsBehaviour>();
            services.AddScoped<IValidator<EnforcementCreateDto>, EnforcementValidationBehaviour>();

            return services;
        }
    }
}
