using CommonItems.Enums;

namespace CommonItems.Models
{
    public class CrimeUpdateDto
    {
        public string Id { get; set; }
        public CrimeType CrimeType { get; set; }
        public string Description { get; set; }
        public DateTime DateOfCrime { get; set; }
        public string PlaceOfCrime { get; set; }
        public string WitnessName { get; set; }
        public string WitnessEmail { get; set; }
        public Status Status { get; set; }
        public string EnforcementId { get; set; }
    }
}
