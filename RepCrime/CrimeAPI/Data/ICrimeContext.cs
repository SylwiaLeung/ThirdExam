using MongoDB.Driver;

namespace CrimeService.Data
{
    public class ICrimeContext
    {
        IMongoCollection<Crime> Crimes { get; }
    }
}
