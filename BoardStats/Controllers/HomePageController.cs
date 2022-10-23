using BoardStats.Data.Services;
using BoardStats.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace BoardStats.Controllers
{
    public class HomePageController : Controller
    {
        private readonly IGameStatService _service;
        public HomePageController(IGameStatService service)
        {
            _service = service;
        }
    
        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
         
            var model = _service.LastGameStats(userId);
            if(model.gameName==null)
            {
                
                model.when = "Nie rozegrano gier";
                model.duration = 0;
                model.whoWin = "Nie rozegrano gier";
                model.when = "Nie rozegrano gier";
                model.gameName= "Nie rozegrano gier";
                MatchVM match = new MatchVM();
                match.GameName = "Nie rozegrano gier";
                match.StartDate = "Nie rozegrano gier";
                match.WhoWIn = "Nie rozegrano gier";
                List<MatchVM> hold = new List<MatchVM>();
                hold.Add(match);
                model.last10Games = hold;
                model.imgUrl = "https://cf.geekdo-images.com/zxVVmggfpHJpmnJY9j-k1w__itemrep/img/Py7CTY0tSBSwKQ0sgVjRFfsVUZU=/fit-in/246x300/filters:strip_icc()/pic1657689.jpg";


            }
            var dropdown = await _service.GetAllGames();
            ViewBag.Games = new SelectList(dropdown.Where(m => m.Expansion == false), "Name", "Name");
            return View(model);
        }
    }
}
