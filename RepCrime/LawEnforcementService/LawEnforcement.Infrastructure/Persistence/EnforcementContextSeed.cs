using LawEnforcement.Domain;
using LawEnforcement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LawEnforcement.Infrastructure.Persistence
{
    public class EnforcementContextSeed
    {
        public static void Initialize()
        {
            var options = new DbContextOptionsBuilder<EnforcementContext>()
                .UseInMemoryDatabase(databaseName: "EnforcementDb")
                .Options;

            using (var context = new EnforcementContext(options))
            {
                if (!context.Enforcement.Any())
                {
                    context.Enforcement.AddRange(GetEnforcement());
                    context.SaveChanges();
                }
            }
        }

        private static IEnumerable<Enforcement> GetEnforcement()
        {
            var enforcement = new List<Enforcement>()
            {
                new Enforcement()
                {
                    Id = "XJDS23",
                    Name = "Jan Kowalski",
                    Rank = Rank.Bad
                },
                new Enforcement()
                {
                    Id = "KHGD14",
                    Name = "Dominik Starzyk",
                    Rank = Rank.Terrific
                },
                new Enforcement()
                {
                    Id = "AWOQ87",
                    Name = "Stefan Nowak",
                    Rank = Rank.Good
                }
            };
            return enforcement;
        }
    }
}
