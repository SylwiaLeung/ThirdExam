namespace LawEnforcement.Domain.Common
{
    public abstract class EntityBase
    {
        public string Id { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
