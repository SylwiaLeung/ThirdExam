using LawEnforcement.Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace LawEnforcement.Infrastructure.Persistence
{
    public class EnforcementContext : DbContext
    {
        public EnforcementContext(DbContextOptions<EnforcementContext> options) : base(options)
        {
        }

        public DbSet<Enforcement> Enforcement { get; set; }
    }
}
