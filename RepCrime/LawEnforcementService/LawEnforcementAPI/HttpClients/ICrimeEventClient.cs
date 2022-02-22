using EventBus.Messaging.Events;

namespace LawEnforcementAPI.HttpClients
{
    public interface ICrimeEventClient
    {
        Task SendUpdatedCrime(CrimeEvent crime);
    }
}
