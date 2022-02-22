using CommonItems.Models;
using System.Text;
using System.Text.Json;

namespace LawEnforcementAPI.HttpClients
{
    public class CrimeEventClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<CrimeEventClient> _logger;

        public CrimeEventClient(HttpClient httpClient, IConfiguration configuration, ILogger<CrimeEventClient> logger)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task SendUpdatedCrime(CrimeUpdateDto crime)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(crime),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PutAsync($"{_configuration["CrimeService"]}/{crime.Id}", httpContent);

            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("--> Sync PUT to CrimeService was OK");
            }
            else
            {
                _logger.LogInformation("--> Sync PUT to CrimeService went wrong...");
            }
        }
    }
}
