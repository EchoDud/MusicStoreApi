using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicStoreApi.Services;

namespace MusicStoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VisitStatsController : ControllerBase
    {
        private readonly IVisitStatService _service;

        public VisitStatsController(IVisitStatService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> GetStats()
        {
            var stats = await _service.GetAllStatsAsync();
            return Ok(stats.Select(s => new
            {
                Month = s.Month.ToString("yyyy-MM"), // Возвращаем в формате YYYY-MM
                s.VisitsCount
            }));
        }

        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> AddOrUpdateStat([FromBody] VisitStatRequest request)
        {
            if (request == null || request.VisitsCount < 0)
            {
                return BadRequest("Invalid data");
            }

            // Преобразуем строку формата YYYY-MM в DateTime
            if (!DateTime.TryParseExact(request.Month, "yyyy-MM", null, System.Globalization.DateTimeStyles.None, out var month))
            {
                return BadRequest("Invalid month format. Use 'yyyy-MM'.");
            }

            await _service.AddOrUpdateStatAsync(month, request.VisitsCount);
            return Ok("Stat updated successfully");
        }
    }

    public class VisitStatRequest
    {
        public string Month { get; set; } = null!; // Формат: YYYY-MM
        public int VisitsCount { get; set; }
    }
}
