using LawEnforcement.Application.Contracts;
using LawEnforcement.Infrastructure.Persistence;
using LawEnforcement.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LawEnforcement.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddDbContext<EnforcementContext>(options => options.UseInMemoryDatabase(databaseName: "EnforcementDb"));
            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
            services.AddScoped<IEnforcementRepository, EnforcementRepository>();

            return services;
        }
    }
}
