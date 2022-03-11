using BoardStats.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BoardStats.Controllers
{
    public class GamesController : Controller
    {

        private readonly ApplicationDbContext _db;

        public GamesController(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task <IActionResult> Index()
        {

            var AllGames = await _db.BoardGames.ToListAsync();
            var AllGames2 = await _db.Collections.ToListAsync();
            foreach (var item in AllGames)
            {
                foreach(var item2 in AllGames2)
                {
                    if(item.IdGame== item2.IdGame)
                        item.IsInCollection = true;
                }
            }
           
            return View(AllGames);
        }
    }
}
