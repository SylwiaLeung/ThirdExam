namespace LawEnforcement.Domain.DTO
{
    public class EnforcementReadDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Rank Rank { get; set; }
        public ICollection<CrimeEvent> Crimes { get; set; }
    }
}
