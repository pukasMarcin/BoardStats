using BoardStats.Data.Services;
using BoardStats.Models;
using Microsoft.AspNetCore.Mvc;

namespace BoardStats.Controllers
{
    public class WinConsController : Controller
    {
        private readonly IWinConService _service;

        public WinConsController(IWinConService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {


            var wincons = await _service.GetAllWinConsAsync();


            return View(wincons);
           
        }

        [HttpGet]
        public IActionResult PopUpWindowNewWinCon()
        {

            WinCon newWinCon = new WinCon();
           

            return PartialView("_PopUpWindowNewWinCon", newWinCon);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewWinCon(WinCon model)
        {
            await _service.AddNewWinConAsync(model);
            return RedirectToAction("Index", "WinCons");
        }


        [HttpGet]
        public IActionResult PopUpWindowDeleteWinCon(int Id)
        {
            var wincon = _service.GetWinConById(Id);
            return PartialView("_PopUpWindowDeleteWinCon", wincon);
        }

        [HttpPost]
        public async Task <IActionResult>Delete(WinCon model)
        {
            await _service.Delete(model.IdWinCon);
            return RedirectToAction("Index", "WinCons");
        }

        [HttpGet]
        public IActionResult PopUpWindowUpdateWinCon(int Id)
        {
            var wincon = _service.GetWinConById(Id);
            return PartialView("_PopUpWindowUpdateWinCon", wincon);

        }

        [HttpPost]
        public async Task<IActionResult>Update(WinCon model)
        {
            await _service.Update(model);
            return RedirectToAction("Index", "WinCons");
        }

    }
}
