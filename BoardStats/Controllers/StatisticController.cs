using BoardStats.Data.Services;
using BoardStats.Data.ViewModels;
using BoardStats.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace BoardStats.Controllers
{
    public class StatisticController : Controller

    {
        private readonly IStatisticService _service;
        public StatisticController(IStatisticService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);

            var check = _service.CheckHistory(userId);
            if(check ==0)
            {
                return RedirectToAction("Index", "Matches", new { Id = "1" });
            }
            else
            {
                var dropdown =   await _service.GetAllGames(userId);
            var dropdown_player = await _service.GetPlayersDroopDown(userId);
            List<PlayerVM> drop_player = new List<PlayerVM>();
            PlayerVM description_player = new PlayerVM();
            description_player.PlayerName = "Filtruj po graczu";
            description_player.HoldId = -1;
            drop_player.Add(description_player);
            foreach (var item in dropdown_player)
            {
                drop_player.Add(item);
            }
            ViewBag.Players = new SelectList(drop_player, "HoldId", "PlayerName");
            List<Boardgames> drop = new List<Boardgames>();
            Boardgames description_game = new Boardgames();
            description_game.IdGame = -1;
            description_game.Expansion = false;
            description_game.Name = "Statystyki dla wybranej gry";
            drop.Add(description_game);
            foreach (var item in dropdown)
            {
                drop.Add(item);
            }
            ViewBag.Games = new SelectList(drop.Where(m => m.Expansion == false), "IdGame", "Name");

            var model = await _service.GetBasicStats(userId);
            var cats = model.playedCategories.Select(m => m.Category);
            ViewBag.Categories = cats;
            var am = model.playedCategories.Select(m => m.playedAmount);
            ViewBag.PlayedAmount = am;
            var topGamesN = model.topGames.Select(m => m.Name);
            ViewBag.topGamesNames = topGamesN;
            var topGamesA = model.topGames.Select(m => m.playedAmount);
            ViewBag.topGamesAmount = topGamesA;
            var playersN = model.playedPlayers.Select(m => m.PlayerName);
            ViewBag.playersName = playersN;
            var playersA= model.playedPlayers.Select(m => m.matchesPlayed);
            ViewBag.playersAmount = playersA;

            var playersWinN = model.top5Winners.Select(m => m.Stat);
            ViewBag.playersWinName = playersWinN;
            var playersWinA = model.top5Winners.Select(m => m.ValueI);
            ViewBag.playersWinAmount = playersWinA;
            return View(model);
            }

            
        }

     

    }
}
