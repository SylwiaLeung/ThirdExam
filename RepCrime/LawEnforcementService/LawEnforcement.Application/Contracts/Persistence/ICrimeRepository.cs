using EventBus.Messaging.Events;

namespace LawEnforcement.Application.Contracts
{
    public interface ICrimeRepository : IAsyncRepository<CrimeEvent>
    {
    }
}
