using CrimeService.Models;
using MongoDB.Driver;

namespace CrimeService.Data
{
    public class CrimeContext : ICrimeContext
    {
        public CrimeContext(IConfiguration configuration, ILogger<HealthbookContext> logger)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            Healthbooks = database.GetCollection<Healthbook>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
            HealthbookContextSeed.SeedData(Healthbooks, logger);
        }

        public IMongoCollection<Crime> Crimes { get; }
    }
}
