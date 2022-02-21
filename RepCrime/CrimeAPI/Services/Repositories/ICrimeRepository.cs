using CrimeService.Models;

namespace CrimeService.Services.Repositories
{
    public interface ICrimeRepository
    {
        Task<IEnumerable<Crime>> GetCrimes();
        Task<Crime> GetCrimeById(string id);
        Task AddCrime(Crime healthbook);
    }
}
