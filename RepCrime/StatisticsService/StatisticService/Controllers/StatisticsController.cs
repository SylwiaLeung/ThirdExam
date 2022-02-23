using CommonItems.Dtos;
using Microsoft.AspNetCore.Mvc;
using StatisticService.HttpClients;

namespace StatisticService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly ICrimesHttpClient _client;

        public StatisticsController(ICrimesHttpClient client)
        {
            _client = client;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnforcementStatsReadDto>>> GetCrimeStatistics()
        {
            var stats = await _client.GetNumberOfCrimesPerOfficer();

            return Ok(stats);
        }
    }
}
