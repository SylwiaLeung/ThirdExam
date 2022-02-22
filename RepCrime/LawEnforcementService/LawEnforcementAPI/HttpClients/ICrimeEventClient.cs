using CommonItems.Models;

namespace LawEnforcementAPI.HttpClients
{
    public interface ICrimeEventClient
    {
        Task SendUpdatedCrime(CrimeUpdateDto crime);
    }
}
