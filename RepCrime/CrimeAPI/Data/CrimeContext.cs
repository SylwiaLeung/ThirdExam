using CrimeService.Models;
using MongoDB.Driver;

namespace CrimeService.Data
{
    public class CrimeContext : ICrimeContext
    {
        public CrimeContext(IConfiguration configuration, ILogger<CrimeContext> logger)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            Crimes = database.GetCollection<Crime>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
            CrimeContextSeed.SeedData(Crimes, logger);
        }

        public IMongoCollection<Crime> Crimes { get; }
    }
}
