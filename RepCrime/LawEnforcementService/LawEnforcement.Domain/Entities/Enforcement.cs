using EventBus.Messaging.Events;

namespace LawEnforcement.Domain.Entities
{
    public class Enforcement
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Rank Rank { get; set; }
        public ICollection<CrimeEvent> Crimes { get; set; } = new List<CrimeEvent>();
    }
}
