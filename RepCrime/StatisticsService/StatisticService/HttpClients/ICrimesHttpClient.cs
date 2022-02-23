using System.Collections.Generic;

namespace StatisticService.HttpClients
{
    public interface ICrimesHttpClient
    {
        Task<Dictionary<DateTime, int>> GetDailyCrimeRate();
        Task<Dictionary<string, int>> GetNumberOfCrimesPerOfficer();
    }
}
