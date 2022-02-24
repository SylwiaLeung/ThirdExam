using LawEnforcement.Domain.Entities;

namespace LawEnforcement.Application.Contracts
{
    public interface IEnforcementRepository : IAsyncRepository<Enforcement>
    {
    }
}
