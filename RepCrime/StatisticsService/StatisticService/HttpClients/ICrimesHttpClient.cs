using CommonItems.Dtos;

namespace StatisticService.HttpClients
{
    public interface ICrimesHttpClient
    {
        Task<IEnumerable<EnforcementStatsReadDto>> GetNumberOfCrimesPerOfficer();
    }
}
