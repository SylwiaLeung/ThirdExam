using CommonItems.Dtos;
using System.Net.Http.Formatting;

namespace StatisticService.HttpClients
{
    public class CrimesHttpClient : ICrimesHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<CrimesHttpClient> _logger;

        public CrimesHttpClient(HttpClient httpClient, IConfiguration configuration, ILogger<CrimesHttpClient> logger)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<IEnumerable<EnforcementStatsReadDto>> GetNumberOfCrimesPerOfficer()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"{_configuration["EnforcementService"]}/unitstats");
            var response = await _httpClient.SendAsync(requestMessage);

            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("--> Sync GET from EnforcementService was OK");
                return await response.Content.ReadAsAsync<List<EnforcementStatsReadDto>>(new[] { new JsonMediaTypeFormatter() });
            }
            else
            {
                throw new BadHttpRequestException("Couldn't send a request synchronously...");
            }
        }
    }
}
