using CommonItems.Enums;

namespace EventBus.Messaging.Events
{
    public class CrimeUpdateEvent
    {
        public string Id { get; set; }
        public string WitnessName { get; set; }
        public string WitnessEmail { get; set; }
        public Status Status { get; set; }
    }
}
