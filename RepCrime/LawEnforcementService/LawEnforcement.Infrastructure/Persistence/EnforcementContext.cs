using EventBus.Messaging.Events;
using LawEnforcement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LawEnforcement.Infrastructure.Persistence
{
    public class EnforcementContext : DbContext
    {
        public EnforcementContext(DbContextOptions<EnforcementContext> options) : base(options)
        {
        }

        public DbSet<Enforcement> Enforcement { get; set; }
        public DbSet<CrimeEvent> CrimeEvents { get; set; }
    }
}
