using EventBus.Messaging.Events;

namespace LawEnforcement.Application.HttpClients
{
    public interface ICrimeEventClient
    {
        Task SendUpdatedCrime(CrimeEvent crime);
    }
}
