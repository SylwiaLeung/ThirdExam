using CommonItems.Enums;
using CrimeService.Models;
using MongoDB.Driver;

namespace CrimeService.Data
{
    public static class CrimeContextSeed
    {
        public static void SeedData(IMongoCollection<Crime> crimeCollection, ILogger logger)
        {
            bool existCrimes = crimeCollection.Find(h => true).Any();
            if (!existCrimes)
            {
                logger.LogInformation("---> Seeding Crimes...");
                crimeCollection.InsertMany(GetCrimes());
            }
        }

        private static IEnumerable<Crime> GetCrimes()
        {
            return new List<Crime>()
            {
                new Crime()
                {
                    Id = "602d2149e773f2a3990b47f1",
                    CrimeType = CrimeType.TrafficAccident,
                    PlaceOfCrime = "Krakow, Kosciuszki 76",
                    WitnessEmail = "email@random.com",
                    EnforcementId = "XJDS23",
                    Description = "It's baaaaad"
                },
                new Crime()
                {
                    Id = "602d2149e773f2a3990b47f2",
                    CrimeType = CrimeType.Burglary,
                    PlaceOfCrime = "Krakow, Salwatorska 123",
                    WitnessEmail = "somsiad@somsiad.pl",
                    EnforcementId = "KHGD14",
                    Description = "I saw it with me own eyes"
                },
                new Crime()
                {
                    Id = "602d2149e773f2a3990b47f3",
                    CrimeType = CrimeType.Homicide,
                    PlaceOfCrime = "Krakow, Rynek 9",
                    WitnessEmail = "tourist@fromabroad.com",
                    EnforcementId = "AWOQ87",
                    Description = "Can't believe this really happened"
                }
            };
        }
    }
}