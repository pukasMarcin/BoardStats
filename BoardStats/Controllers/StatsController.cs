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

   

        [HttpPost]
        public async Task<IActionResult> AddNewStat(Stat model)
        {
            await _service.AddNewStatAsync(model);
            return RedirectToAction("Index", "Stats");
        }


        [HttpGet]
        public IActionResult PopUpWindowDeleteStat(int Id)
        {
            var stat = _service.GetStatById(Id);
            return PartialView("_PopUpWindowDeleteStat", stat);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Stat model)
        {
            await _service.Delete(model.IdStat);
            return RedirectToAction("Index", "Stats");
        }

        [HttpGet]
        public IActionResult PopUpWindowUpdateStat(int Id)
        {
            var stat = _service.GetStatById(Id);
            return PartialView("_PopUpWindowUpdateStat", stat);

        }

        [HttpPost]
        public async Task<IActionResult> Update(Stat model)
        {
            await _service.Update(model);
            return RedirectToAction("Index", "Stats");
        }

    }
}
