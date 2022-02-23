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

        public async Task<Dictionary<DateTime, int>> GetDailyCrimeRate()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"{_configuration["CrimeService"]}/");
            var response = await _httpClient.SendAsync(requestMessage);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(await response.Content.ReadAsStringAsync());
                return await response.Content.ReadAsAsync<Dictionary<DateTime, int>>(new[] { new JsonMediaTypeFormatter() });
            }
            else
            {
                return null;
            }
        }

        public Task<Dictionary<string, int>> GetNumberOfCrimesPerOfficer()
        {
            throw new NotImplementedException();
        }
    }
}
