using BoardStats.Data.Services;
using BoardStats.Data.ViewModels;
using BoardStats.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BoardStats.Controllers
{
    public class PlayersController : Controller
    {

        public readonly IPlayersService _service;

        public PlayersController(IPlayersService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);
            var players = await _service.GetAllPlayersByUserId(userId, userRole);

            return View(players);
        }


        public IActionResult AddPlayer()
        {
            return View();
        }

        [HttpGet]
        public IActionResult PopUpWindow2()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ApplicationUser us = _service.GetUser(userId);
            NewPlayerVM newPlayer = new NewPlayerVM()
            {
                PlayerTag = "Player",
                UserName = us.UserName,
                UserId = userId,


            };

            return PartialView("_PopUpWindow2",newPlayer);
        }

        [HttpGet]
        public IActionResult PopUpWindowUpdatePlayer(int Id)
        {
            var player = _service.GetById(Id);

            return PartialView("_PopUpWindowUpdatePlayer", player);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewPlayer2(NewPlayerVM model)
        {
            await _service.AddNewPlayerAsync(model);
            return RedirectToAction("Index", "Players");
        }

        [HttpPost]
        public async Task<IActionResult>Update(Player model)
        {
            await _service.UpdatePlayerAsync(model);
            return RedirectToAction("Index", "Players");
        }

        [HttpGet]
        public IActionResult PopUpWindowDeletePlayer(int Id)
        {
            var player = _service.GetById(Id);
            return PartialView("_PopUpWindowDeletePlayer", player);

        }

        [HttpPost]
        public async Task<IActionResult>Delete(Player model)
        {
            await _service.DeletePlayer(model);
            return RedirectToAction("Index", "Players");
        }
    }
}
