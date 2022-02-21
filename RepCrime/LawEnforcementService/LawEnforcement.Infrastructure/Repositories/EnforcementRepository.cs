using LawEnforcement.Application.Contracts;
using LawEnforcement.Domain.Entities;
using LawEnforcement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LawEnforcement.Infrastructure.Repositories
{
    public class EnforcementRepository : RepositoryBase<Enforcement>, IEnforcementRepository
    {
        private readonly EnforcementContext _context;

        public EnforcementRepository(EnforcementContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IReadOnlyList<Enforcement>> GetAllAsync()
        {
            return await _context.Enforcement.Include(e => e.Crimes).ToListAsync();
        }
    }
}
