using LawEnforcement.Application.Contracts;
using LawEnforcement.Domain.Entities;
using LawEnforcement.Infrastructure.Persistence;

namespace LawEnforcement.Infrastructure.Repositories
{
    public class EnforcementRepository : RepositoryBase<Enforcement>, IEnforcementRepository
    {
        public EnforcementRepository(EnforcementContext context) : base(context)
        {
        }
    }
}
