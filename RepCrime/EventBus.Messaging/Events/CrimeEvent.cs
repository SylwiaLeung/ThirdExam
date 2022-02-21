using CommonItems.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messaging.Events
{
    public class CrimeEvent
    {
        public string Id { get; set; }
        public CrimeType CrimeType { get; set; }
        public string Description { get; set; }
        public DateTime DateOfCrime { get; set; }
        public string PlaceOfCrime { get; set; }
        public string WitnessEmail { get; set; }
        public Status Status { get; set; }
        public string EnforcementId { get; set; }
    }
}
