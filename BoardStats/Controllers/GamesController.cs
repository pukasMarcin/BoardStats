using AppointmentScheduling.Models.ViewModels;
using BoardStats.Data.Services;
using BoardStats.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using System.Xml;
using BoardStats.Utility;
using Newtonsoft.Json.Linq;
using BoardStats.Data.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using CheckBoxList.Core.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace BoardStats.Controllers
{
    [Authorize]
    public class GamesController : Controller
    {

        private readonly IGamesService _serviceBg;
        private readonly ICollectionService _serviceCol;

        public GamesController(IGamesService serviceBg, ICollectionService serviceCol)
        {
            _serviceBg = serviceBg;
            _serviceCol = serviceCol;
        }
        public async Task<IActionResult> Index()
        {

            var allGames = await _serviceBg.GetAll();
      
            int order = 1;
            foreach (var item in allGames)
            {
                item.OrderNumber = order;
                order++;
            }





            return View(allGames);
        }

       


        [HttpPost("Test")]
        [Route("Test")]
        public IActionResult Test([FromBody] Collection col)
        {

            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Games");
            }
            _serviceCol.AddCollection(col);
            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public IActionResult PopUpWindow(int Id)
        {

            var Game = _serviceBg.GetById(Id);

            return PartialView("_PopUpGame", Game);
        }

   

        public IActionResult AddNewGame()
        {





            return View(AddNewGame);
        }



        public IActionResult AddGameBgg(string id)
        {
            if (id == "2")
            {
                ViewBag.Error = "Nie ma takiej gry w serwisie BGG";
                ViewData["BbgId"] = "Podaj Id Bbg";
                return View(AddGameBgg);
            }
            else
            {
                ViewData["BbgId"] = "Podaj Id Bbg";
                return View(AddGameBgg);
            }

        }

     

        [HttpPost]
        public IActionResult CheckBgg(string Bgg_Id)
        {
            string filename = "https://boardgamegeek.com/xmlapi2/thing?id=";
            string filename2 = "?stats=1";

            XmlDocument doccc = new XmlDocument();
            doccc.Load(filename + Bgg_Id + filename2);
            XmlNodeList elemList = doccc.GetElementsByTagName("maxplayers");
            if (elemList.Count != 0)
            {
                return RedirectToAction("SeedBgg", new RouteValueDictionary(
    new { controller = "Games", action = "SeedBgg", Bgg_Id = Bgg_Id }));
            }
            else

            {
                
                return RedirectToAction("AddGameBgg", new RouteValueDictionary(
   new { controller = "Games", action = "AddGameBgg", id= "2" }));
                
            }


        }

        public async Task <IActionResult> SeedBgg(string Bgg_Id)
        {
            BoardViewModel keeper = new BoardViewModel();
            var gameDroop = await _serviceBg.GetNewGameDroopdownsValues();
            var statsDroop = await _serviceBg.GetNewGameDroopdownStats();
            var winDroop = await _serviceBg.GetNewGameDroopdownWins();

          



            ViewBag.Win = new SelectList(gameDroop.WinCons, "IdWinCon", "WinCondition");

            //ViewBag.lala = new SelectList(statsDroop, "StatsId", "Statistic", "isChecked");

            ViewBag.Stats = new SelectList(gameDroop.Stats, "IdStat", "Statistic");

            var AllGames = await _serviceBg.GetAll();
            ViewBag.Names = new SelectList(AllGames.Where(m => m.Expansion==false).Select(t => t.Name));

            keeper = _serviceBg.Seeding(Bgg_Id);

                keeper.statsVM = statsDroop;
            keeper.winVM= winDroop;

                return View("AddNewGame", keeper);
            
           

        }

      public async Task<IActionResult> NoBgg()
        {
            BoardViewModel keeper = new BoardViewModel();
            var gameDroop = await _serviceBg.GetNewGameDroopdownsValues();
            var statsDroop = await _serviceBg.GetNewGameDroopdownStats();
            var winDroop = await _serviceBg.GetNewGameDroopdownWins();




            ViewBag.Win = new SelectList(gameDroop.WinCons, "IdWinCon", "WinCondition");

            //ViewBag.lala = new SelectList(statsDroop, "StatsId", "Statistic", "isChecked");

            ViewBag.Stats = new SelectList(gameDroop.Stats, "IdStat", "Statistic");
            var AllGames = await _serviceBg.GetAll();
            ViewBag.Names = new SelectList(AllGames.Where(m => m.Expansion == false).Select(t => t.Name));


            keeper.statsVM = statsDroop;
            keeper.winVM = winDroop;
            return View("AddNewGame", keeper);
            

        }

        [HttpPost]
        public async Task<IActionResult> Create(BoardViewModel board)
        {
           

            await _serviceBg.AddNewGameAsync(board);
            return RedirectToAction("Index", "Games");

        }
       
      
        public async Task< IActionResult> Update(int Id)
        {

            var game = _serviceBg.GetById(Id);
          


            BoardViewModel keeper = new BoardViewModel();

            keeper.Name = game.Name;
            keeper.holdId = game.IdGame;
            keeper.BggRate = game.BggRate;
            keeper.MinPlayers = game.MinPlayers;
            keeper.MaxPlayers = game.MaxPlayers;
            keeper.BestPlayers = game.BestPlayers;
            keeper.Description = game.Description;
            keeper.PlayingTime = game.PlayingTime;
            keeper.ImageUrl = game.ImageUrl;
            keeper.BggId = game.BggId;
            keeper.InstructionUrl = game.InstructionUrl;
            keeper.Category=game.Category;  
            keeper.Expansion=game.Expansion;
            keeper.MainGame=game.MainGame;
            keeper.IsInCollection=game.IsInCollection;
            keeper.OrderNumber=game.OrderNumber;
            keeper.Stat_Ids=game.Game_Stat.Select(n => n.StatId).ToList();
            keeper.Win_Ids = game.Game_Win.Select(n => n.WinConId).ToList();

          
            var statsDroop = await _serviceBg.GetNewGameDroopdownStats();
            keeper.statsVM = statsDroop;
            
            var winDroop = await _serviceBg.GetNewGameDroopdownWins();
            keeper.winVM = winDroop;

            for (int i = 0; i < keeper.Win_Ids.Count; i++)
            {
                for (int j = 0; j < keeper.winVM.Count; j++)
                {
                    if (keeper.Win_Ids[i] == keeper.winVM[j].WinId) keeper.winVM[j].isChecked = true;
                }
            };

            for (int i = 0; i < keeper.Stat_Ids.Count; i++)
            {
                for (int j = 0; j < keeper.statsVM.Count; j++)
                {
                    if (keeper.Stat_Ids[i] == keeper.statsVM[j].StatsId) keeper.statsVM[j].isChecked = true;
                }
            };



            var AllGames = await _serviceBg.GetAll();
            ViewBag.Names = new SelectList(AllGames.Where(m => m.Expansion == false).Select(t => t.Name));

            return View("UpdateGame", keeper);
        }


        [HttpPost]
        public async Task<IActionResult> Update( BoardViewModel board)
        {
            await _serviceBg.UpdateGameAsync(board);
            return RedirectToAction("Index", "Games");

        }

        [HttpPost]
        public async Task<IActionResult> DeleteGame(int Id)
        {
            await _serviceBg.DeleteGameAsync(Id);
            return RedirectToAction("Index", "Games");

        }


        public async Task<IActionResult> Filter(string searchString)
        {

            var allGames = await _serviceBg.GetAll();
            int order = 1;
            foreach (var item in allGames)
            {
                item.OrderNumber = order;
                order++;
            }


            if (!string.IsNullOrEmpty(searchString))
            {
                IEnumerable<Boardgames> filteredResult = allGames.Where(m =>m.Name.ToLower().Contains(searchString.ToLower()) || m.Description.ToLower().Contains(searchString.ToLower())).ToList();
                int order2 = 1;
                foreach (var item in filteredResult)
                {
                    item.OrderNumber = order2;
                    order2++;
                }

                return View("Index",filteredResult);
            }
            return View("Index",allGames);
        }

       

    }
}
