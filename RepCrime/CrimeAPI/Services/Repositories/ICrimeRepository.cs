using CrimeService.Models;

namespace CrimeService.Services.Repositories
{
    public interface ICrimeRepository
    {
        Task<IEnumerable<Crime>> GetCrime();
        Task<Crime> GetCrimeById(string id);
        Task AddCrime(Crime healthbook);
    }
}
