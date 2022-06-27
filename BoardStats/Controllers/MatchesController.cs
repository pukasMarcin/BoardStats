using BoardStats.Data.Services;
using BoardStats.Data.ViewModels;
using BoardStats.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace BoardStats.Controllers
{
    public class Matches : Controller
    {
        public readonly IMatchesServices _service;
        public Matches(IMatchesServices service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);

            var matches = await _service.GetAllMatchesById(userId, userRole);

            return View(matches);
        }

        public async Task<IActionResult> AddMatch()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var dropdown = await _service.GetAllGames();

            ViewBag.Games = new SelectList(dropdown.Where(m => m.Expansion==false), "IdGame", "Name");

            MatchVM NewMatch = new MatchVM();

            var playerList = await _service.GetPlayersDroopDown(userId);
            NewMatch.PlayerVM = playerList;
            return View(NewMatch);
        }

        public async Task<IActionResult> AddMatch2(MatchVM model)
        {
            List<Challange> challangeList = new List<Challange>();
            challangeList = await _service.GetChallanges();
            ViewBag.Challanges = new SelectList(challangeList,"ChallangeId","ChallangeName");
            model.ExpansionsVM = await _service.GetExpansionsByName(model.IdGame);
            ViewBag.Players = new SelectList(model.PlayerVM.Where(m => m.isChecked==true), "HoldId", "PlayerName");
            List<int> list = new List<int>();
            list.Add(model.IdGame);
            if (model.ExpansionsVM != null)
            {
                foreach (var item in model.ExpansionsVM)
                {
                    list.Add(item.HoldId);
                }
            }
            var wins = await _service.GetWins(list);
            ViewBag.Wins = new SelectList(wins, "IdWinCon", "WinCondition");
            return View(model);
        }

        public async Task<IActionResult> AddMatch3(MatchVM model)
        {

            List<int> list = new List<int>();
            list.Add(model.IdGame);
            if (model.ExpansionsVM != null)
            {
                foreach (var item in model.ExpansionsVM)
                {
                    list.Add(item.HoldId);
                }
            }
            var stats = await _service.GetStats(list);

            List<StatsVM> listStats = new List<StatsVM>();
            List<StatsVM> listPlayerStats = new List<StatsVM>();
            foreach (var item in stats)
            {
                var mt_st=new StatsVM();

               
                mt_st.Statistic=item.Statistic;
                mt_st.StatsId = item.IdStat;
                mt_st.isChecked = true;
                mt_st.Category=item.Category;
                listStats.Add(mt_st);    

            }
            listStats.Count();
            model.StatsVM = listStats;
            MatchStatVM match_st = new MatchStatVM();
            match_st.Statistic = "Test";
            List<MatchStatVM> listMatch_st = new List<MatchStatVM>();
            List<PlayerVM> listPlayer = new List<PlayerVM>();
            for (int i=0;i<model.PlayerVM.Count();i++)
            {
                if (model.PlayerVM[i].isChecked == true)
                   listPlayer.Add(model.PlayerVM[i]);
                    
            }
            model.PlayerVM.Clear();

            model.PlayerVM2 = listPlayer;

            for (int j = 0; j < listStats.Count(); j++)
            {
                if (listStats[j].Category == "Player")
                   listPlayerStats.Add(listStats[j]);
            }

            for (int i=0;i< model.PlayerVM2.Count(); i++)
            {
                
                    model.PlayerVM2[i].StatsVM = listPlayerStats;
                
                
            }
            
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult>Create(MatchVM model)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string name = _service.GetGameById(model.IdGame);
            model.GameName = name;
            model.UserId = userId;
            var us = _service.GetUser(userId);
            model.UserName = us.UserName;
            await _service.AddNewMatch(model);
            return RedirectToAction("Index", "Matches");

        }

        [HttpGet]
        public IActionResult PopUpWindowDeleteMatch(int Id)
        {
            var match = _service.GetById(Id);
            return PartialView("_PopUpWindowDeleteMatch", match);

        }

        [HttpPost]
        public async Task<IActionResult> Delete(Match model)
        {
            await _service.DeleteMatch(model);
            return RedirectToAction("Index", "Matches");
        }
    }
}
