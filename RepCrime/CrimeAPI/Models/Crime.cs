using CommonItems.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CrimeService.Models
{
    public class Crime
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public CrimeType CrimeType { get; set; }
        public string? Description { get; set; }
        public DateTime DateOfCrime { get; set; } = DateTime.UtcNow;
        public string PlaceOfCrime { get; set; }
        public string? WitnessEmail { get; set; }
        public Status Status { get; set; } = Status.Waiting;
        public string EnforcementId { get; set; }
    }
}
