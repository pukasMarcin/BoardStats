using BoardStats.Data.Services;
using BoardStats.Data.ViewModels;
using BoardStats.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Security.Claims;

using Microsoft.AspNetCore.Mvc;

namespace BoardStats.Controllers
{
    public class PlayerStatsController : Controller
    {
        private readonly IPlayerStatsService _service;
        public PlayerStatsController(IPlayerStatsService service)
        {
            _service = service;
        }
        public IActionResult Index(int Id)
        {
            var model = _service.GetPlayerStats(Id);
            if(model.top5Games!=null)
            {
                var topGames = model.top5Games.Select(m => m.ValueS);
                ViewBag.topGames = topGames;
                var topGamesA = model.top5Games.Select(m => m.ValueSAmount);
                ViewBag.topGamesA = topGamesA;
            }
           
            return View(model);
        }
    }
}
