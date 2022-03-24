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

namespace BoardStats.Controllers
{
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

            var AllGames = await _serviceBg.GetAll();
            var AllGames2 = await _serviceCol.GetAll();
            int order = 1;
            foreach (var item in AllGames)
            {
                item.OrderNumber = order;
                order++;
            }





            return View(AllGames);
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
            var statsDroop = await _serviceBg.GetNewGameDroopdownsValues2();

           



            ViewBag.Win = new SelectList(gameDroop.WinCons, "IdWinCon", "WinCondition");

            //ViewBag.lala = new SelectList(statsDroop, "StatsId", "Statistic", "isChecked");

            ViewBag.Stats = new SelectList(gameDroop.Stats, "IdStat", "Statistic");

       
                keeper = _serviceBg.Seeding(Bgg_Id);

                keeper.statsVM = statsDroop;

                return View("AddNewGame", keeper);
            
           

        }

      public async Task<IActionResult> NoBgg()
        {
            BoardViewModel keeper = new BoardViewModel();
            var gameDroop = await _serviceBg.GetNewGameDroopdownsValues();
            var statsDroop = await _serviceBg.GetNewGameDroopdownsValues2();





            ViewBag.Win = new SelectList(gameDroop.WinCons, "IdWinCon", "WinCondition");

            //ViewBag.lala = new SelectList(statsDroop, "StatsId", "Statistic", "isChecked");

            ViewBag.Stats = new SelectList(gameDroop.Stats, "IdStat", "Statistic");

          

                keeper.statsVM = statsDroop;
                return View("AddNewGame", keeper);
            

        }

        [HttpPost]
        public async Task<IActionResult> Create(BoardViewModel board)
        {
            await _serviceBg.AddNewGameAsync(board);
            return RedirectToAction("Index", "Games");

        }
       

    }
}
