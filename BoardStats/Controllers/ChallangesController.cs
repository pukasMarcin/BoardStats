using BoardStats.Data.Services;
using BoardStats.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BoardStats.Controllers
{
    public class ChallangesController : Controller
    {
        private readonly IChallangesServices _service;
        public ChallangesController(IChallangesServices service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);
            var challanges = await _service.GetChallangersByUserId(userId, userRole);

            return View(challanges);
        }

        [HttpGet]
        public IActionResult PopUpWindow2()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ApplicationUser us = _service.GetUser(userId);
            Challange newChallange = new Challange()
            {
                
                UserName = us.UserName,
                UserId = userId,


            };

            return PartialView("_PopUpWindow2", newChallange);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewChallange(Challange model)
        {
            await _service.AddNewChallangeAsync(model);
            return RedirectToAction("Index", "Challanges");
        }

        [HttpGet]
        public IActionResult PopUpWindowUpdateChallange(int Id)
        {
            var challange = _service.GetById(Id);

            return PartialView("_PopUpWindowUpdateChallange", challange);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Challange model)
        {
            await _service.UpdateChallangeAsync(model);
            return RedirectToAction("Index", "Challanges");
        }

        [HttpGet]
        public IActionResult PopUpWindowDeleteChallange(int Id)
        {
            var challange = _service.GetById(Id);
            return PartialView("_PopUpWindowDeleteChallange", challange);

        }

        [HttpPost]
        public async Task<IActionResult> Delete(Challange model)
        {
            await _service.DeleteChallange(model);
            return RedirectToAction("Index", "Challanges");
        }
    }
}
