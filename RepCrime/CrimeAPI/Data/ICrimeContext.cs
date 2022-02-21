using CrimeService.Models;
using MongoDB.Driver;

namespace CrimeService.Data
{
    public interface ICrimeContext
    {
        IMongoCollection<Crime> Crimes { get; }
    }
}
