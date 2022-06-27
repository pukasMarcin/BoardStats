using BoardStats.Data.Services;
using BoardStats.Models;
using Microsoft.AspNetCore.Mvc;

namespace BoardStats.Controllers
{
    public class StatsController : Controller
    {
        public readonly IStatsService _service;
        public StatsController(IStatsService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var stats = await _service.GetAllStats();

            return View(stats);
        }

        [HttpGet]
        public IActionResult PopUpWindowNewStat()
        {
            Stat model = new Stat();
            return PartialView("_PopUpWindowNewStat", model);
        }
    }
}
