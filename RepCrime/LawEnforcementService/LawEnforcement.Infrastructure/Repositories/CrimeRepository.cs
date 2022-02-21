using EventBus.Messaging.Events;
using LawEnforcement.Application.Contracts;
using LawEnforcement.Infrastructure.Persistence;

namespace LawEnforcement.Infrastructure.Repositories
{
    public class CrimeRepository : RepositoryBase<CrimeEvent>, ICrimeRepository
    {
        public CrimeRepository(EnforcementContext context) : base(context)
        {
        }
    }
}
