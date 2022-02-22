using CommonItems.Enums;

namespace LawEnforcement.Domain.DTO
{
    public class CrimeUpdateDto
    {
        public Status? Status { get; set; }
        public string? EnforcementId { get; set; }
    }
}
