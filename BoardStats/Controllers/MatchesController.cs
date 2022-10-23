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

     
        public async Task<IActionResult> IndexFilterByGame(string Id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);
            var dropdown = await _service.GetAllGames();
            var dropdown_player = await _service.GetPlayersDroopDown(userId);
            var dropdown_winner = await _service.GetAllPlayersDroopDown(userId);
            var dropdown_challanges = await _service.GetChallanges(userId);
            List<Boardgames> drop = new List<Boardgames>();
            List<PlayerVM> drop_player = new List<PlayerVM>();
            List<PlayerVM> drop_winner = new List<PlayerVM>();
            List<Challange> drop_challange = new List<Challange>();
            Boardgames description_game = new Boardgames();
            PlayerVM description_player = new PlayerVM();
            PlayerVM description_winner = new PlayerVM();
            Challange description_challange = new Challange();
            description_game.Name = "Filtruj po grze";
            description_game.IdGame = -1;
            description_game.Expansion = false;
            description_player.PlayerName = "Filtruj po graczu";
            description_player.HoldId = -1;
            description_challange.ChallangeName = "Filtruj po kampanii";
            description_challange.ChallangeId = -1;
            description_winner.PlayerName = "Filtruj po zwyciezscy";
            description_winner.HoldId = -1;

            drop.Add(description_game);
            drop_player.Add(description_player);
            drop_winner.Add(description_winner);
            drop_challange.Add(description_challange);
            foreach (var item in dropdown)
            {
                drop.Add(item);
            }
            foreach (var item in dropdown_player)
            {
                drop_player.Add(item);
            }
            foreach (var item in dropdown_winner)
            {
                drop_winner.Add(item);
            }
            foreach (var item in dropdown_challanges)
            {
                drop_challange.Add(item);
            }
            ViewBag.Games = new SelectList(drop.Where(m => m.Expansion == false), "IdGame", "Name");
            ViewBag.Players = new SelectList(drop_player, "HoldId", "PlayerName");
            ViewBag.Winners = new SelectList(drop_winner, "HoldId", "PlayerName");
            ViewBag.Challanges = new SelectList(drop_challange, "ChallangeId", "ChallangeName");

            var matches = await _service.GetAllMatchesByGame(userId, userRole, Id);

            var countMatches = matches.Count() - 1;
            List<Match> match_result = new List<Match>();
            for (int i = countMatches; i >= 0; i--)
            {
                match_result.Add(matches[i]);
            }



            return View("Index", match_result);

        }
        public async Task<IActionResult> IndexFilterByChallange(string Id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);
            var dropdown = await _service.GetAllGames();
            var dropdown_player = await _service.GetPlayersDroopDown(userId);
            var dropdown_winner = await _service.GetAllPlayersDroopDown(userId);
            var dropdown_challanges = await _service.GetChallanges(userId);
            List<Boardgames> drop = new List<Boardgames>();
            List<PlayerVM> drop_player = new List<PlayerVM>();
            List<PlayerVM> drop_winner = new List<PlayerVM>();
            List<Challange> drop_challange = new List<Challange>();
            Boardgames description_game = new Boardgames();
            PlayerVM description_player = new PlayerVM();
            PlayerVM description_winner = new PlayerVM();
            Challange description_challange = new Challange();
            description_game.Name = "Filtruj po grze";
            description_game.IdGame = -1;
            description_game.Expansion = false;
            description_player.PlayerName = "Filtruj po graczu";
            description_player.HoldId = -1;
            description_challange.ChallangeName = "Filtruj po kampanii";
            description_challange.ChallangeId = -1;
            description_winner.PlayerName = "Filtruj po zwyciezscy";
            description_winner.HoldId = -1;

            drop.Add(description_game);
            drop_player.Add(description_player);
            drop_winner.Add(description_winner);
            drop_challange.Add(description_challange);
            foreach (var item in dropdown)
            {
                drop.Add(item);
            }
            foreach (var item in dropdown_player)
            {
                drop_player.Add(item);
            }
            foreach (var item in dropdown_winner)
            {
                drop_winner.Add(item);
            }
            foreach (var item in dropdown_challanges)
            {
                drop_challange.Add(item);
            }
            ViewBag.Games = new SelectList(drop.Where(m => m.Expansion == false), "IdGame", "Name");
            ViewBag.Players = new SelectList(drop_player, "HoldId", "PlayerName");
            ViewBag.Winners = new SelectList(drop_winner, "HoldId", "PlayerName");
            ViewBag.Challanges = new SelectList(drop_challange, "ChallangeId", "ChallangeName");

            var matches = await _service.GetAllMatchesByChallange(userId, userRole, Id);
            var countMatches = matches.Count() - 1;
            List<Match> match_result = new List<Match>();
            for (int i = countMatches; i >= 0; i--)
            {
                match_result.Add(matches[i]);
            }



            return View("Index", match_result);

        }

        public async Task<IActionResult> IndexFilterByPlayer(string Id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);
            var dropdown = await _service.GetAllGames();
            var dropdown_player = await _service.GetPlayersDroopDown(userId);
            var dropdown_winner = await _service.GetAllPlayersDroopDown(userId);
            var dropdown_challanges = await _service.GetChallanges(userId);
            List<Boardgames> drop = new List<Boardgames>();
            List<PlayerVM> drop_player = new List<PlayerVM>();
            List<PlayerVM> drop_winner = new List<PlayerVM>();
            List<Challange> drop_challange = new List<Challange>();
            Boardgames description_game = new Boardgames();
            PlayerVM description_player = new PlayerVM();
            PlayerVM description_winner = new PlayerVM();
            Challange description_challange = new Challange();
            description_game.Name = "Filtruj po grze";
            description_game.IdGame = -1;
            description_game.Expansion = false;
            description_player.PlayerName = "Filtruj po graczu";
            description_player.HoldId = -1;
            description_challange.ChallangeName = "Filtruj po kampanii";
            description_challange.ChallangeId = -1;
            description_winner.PlayerName= "Filtruj po zwyciezscy";
            description_winner.HoldId = -1;
          
            drop.Add(description_game);
            drop_player.Add(description_player);
            drop_winner.Add(description_winner);
            drop_challange.Add(description_challange);
            foreach (var item in dropdown)
            {
                drop.Add(item);
            }
            foreach (var item in dropdown_player)
            {
                drop_player.Add(item);
            }
            foreach (var item in dropdown_winner)
            {
                drop_winner.Add(item);
            }
            foreach (var item in dropdown_challanges)
            {
                drop_challange.Add(item);
            }
            ViewBag.Games = new SelectList(drop.Where(m => m.Expansion == false), "IdGame", "Name");
            ViewBag.Players = new SelectList(drop_player, "HoldId", "PlayerName");
            ViewBag.Winners = new SelectList(drop_winner, "HoldId", "PlayerName");
            ViewBag.Challanges = new SelectList(drop_challange, "ChallangeId", "ChallangeName");
            var matches = await _service.GetAllMatchesByPlayer(userId, userRole, Id);
            var countMatches = matches.Count() - 1;
            List<Match> match_result = new List<Match>();
            for (int i = countMatches; i >= 0; i--)
            {
                match_result.Add(matches[i]);
            }



            return View("Index", match_result);

        }

        public async Task<IActionResult> IndexFilterByWinner(string Id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);
            var dropdown = await _service.GetAllGames();
            var dropdown_player = await _service.GetPlayersDroopDown(userId);
            var dropdown_winner = await _service.GetAllPlayersDroopDown(userId);
            var dropdown_challanges = await _service.GetChallanges(userId);
            List<Boardgames> drop = new List<Boardgames>();
            List<PlayerVM> drop_player = new List<PlayerVM>();
            List<PlayerVM> drop_winner = new List<PlayerVM>();
            List<Challange> drop_challange = new List<Challange>();
            Boardgames description_game = new Boardgames();
            PlayerVM description_player = new PlayerVM();
            PlayerVM description_winner = new PlayerVM();
            Challange description_challange = new Challange();
            description_game.Name = "Filtruj po grze";
            description_game.IdGame = -1;
            description_game.Expansion = false;
            description_player.PlayerName = "Filtruj po graczu";
            description_player.HoldId = -1;
            description_challange.ChallangeName = "Filtruj po kampanii";
            description_challange.ChallangeId = -1;
            description_winner.PlayerName = "Filtruj po zwyciezscy";
            description_winner.HoldId = -1;

            drop.Add(description_game);
            drop_player.Add(description_player);
            drop_winner.Add(description_winner);
            drop_challange.Add(description_challange);
            foreach (var item in dropdown)
            {
                drop.Add(item);
            }
            foreach (var item in dropdown_player)
            {
                drop_player.Add(item);
            }
            foreach (var item in dropdown_winner)
            {
                drop_winner.Add(item);
            }
            foreach (var item in dropdown_challanges)
            {
                drop_challange.Add(item);
            }
            ViewBag.Games = new SelectList(drop.Where(m => m.Expansion == false), "IdGame", "Name");
            ViewBag.Players = new SelectList(drop_player, "HoldId", "PlayerName");
            ViewBag.Winners = new SelectList(drop_winner, "HoldId", "PlayerName");
            ViewBag.Challanges = new SelectList(drop_challange, "ChallangeId", "ChallangeName");

            var matches = await _service.GetAllMatchesByWinner(userId, userRole, Id);
            var countMatches = matches.Count() - 1;
            List<Match> match_result = new List<Match>();
            for (int i = countMatches; i >= 0; i--)
            {
                match_result.Add(matches[i]);
            }

           

            return View("Index", match_result);

        }
        public async Task<IActionResult> Index(int Id)
        {

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);
            var dropdown = await _service.GetAllGames();
            var dropdown_player = await _service.GetPlayersDroopDown(userId);
            var dropdown_winner = await _service.GetAllPlayersDroopDown(userId);
            var dropdown_challanges = await _service.GetChallanges(userId);
            List<Boardgames> drop = new List<Boardgames>();
            List<PlayerVM> drop_player = new List<PlayerVM>();
            List<PlayerVM> drop_winner = new List<PlayerVM>();
            List<Challange> drop_challange= new List<Challange>();
            Boardgames description_game = new Boardgames();
            PlayerVM description_player = new PlayerVM();
            PlayerVM description_winner = new PlayerVM();
            Challange description_challange = new Challange();
            description_game.Name = "Filtruj po grze";
            description_game.IdGame = -1;
            description_game.Expansion = false;
            description_player.PlayerName = "Filtruj po graczu";
            description_player.HoldId = -1;
            description_challange.ChallangeName = "Filtruj po kampanii";
            description_challange.ChallangeId = -1;
            description_challange.UserId = userId;
            description_challange.UserName = "hold";

            description_winner.PlayerName = "Filtruj po zwyciezscy";
            description_winner.HoldId = -1;

            drop.Add(description_game);
            drop_player.Add(description_player);
            drop_winner.Add(description_winner);
            drop_challange.Add(description_challange);
            foreach (var item in dropdown)
            {
                drop.Add(item);
            }
            foreach (var item in dropdown_player)
            {
                drop_player.Add(item);
            }
            foreach (var item in dropdown_winner)
            {
                drop_winner.Add(item);
            }
            foreach (var item in dropdown_challanges)
            {
                drop_challange.Add(item);
            }
            ViewBag.Games = new SelectList(drop.Where(m => m.Expansion == false), "IdGame", "Name");
            ViewBag.Players = new SelectList(drop_player, "HoldId", "PlayerName");
            ViewBag.Winners = new SelectList(drop_winner, "HoldId", "PlayerName");
            ViewBag.Challanges = new SelectList(drop_challange, "ChallangeId", "ChallangeName");

            if (Id == 0)
            {
                var matches = await _service.GetAllMatchesById(userId, userRole);
                var countMatches = matches.Count() - 1;
                List<Match> match_result = new List<Match>();
                for(int i = countMatches;i>=0;i--)
                {
                    match_result.Add(matches[i]);
                }
               
                return View(match_result);
            }
            else if (Id == 1)
            {
                var matches = await _service.GetAllDoneMatchesById(userId, userRole);
                var countMatches = matches.Count() - 1;
                List<Match> match_result = new List<Match>();
                for (int i = countMatches; i >= 0; i--)
                {
                    match_result.Add(matches[i]);
                }

                return View(match_result);
                
            }
            else return Ok();
        }

        public async Task<IActionResult> AddMatch()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var dropdown = await _service.GetAllGames();

            Challange holdChallange = new Challange();
            holdChallange.ChallangeName = "Jeśli gra jest częścią kampanii, wskaż jakiej:";
            holdChallange.ChallangeId = 1;
            List<Challange> challangeList = new List<Challange>();
            List<Challange> challangeSelector = new List<Challange>();
            challangeSelector.Add(holdChallange);
            challangeList = await _service.GetChallanges();
            foreach(var item in challangeList)
            {
                if(item.ChallangeId != 1)   
                challangeSelector.Add(item);
            }
            ViewBag.Challanges = new SelectList(challangeSelector, "ChallangeId", "ChallangeName");

            ViewBag.Games = new SelectList(dropdown.Where(m => m.Expansion==false), "IdGame", "Name");

            MatchVM NewMatch = new MatchVM();

            var playerList = await _service.GetPlayersDroopDown(userId);
            NewMatch.PlayerVM = playerList;
            return View(NewMatch);
        }

        public async Task<IActionResult> AddMatch2(MatchVM model)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<Challange> challangeList = new List<Challange>();
            challangeList = await _service.GetChallanges();
            ViewBag.Challanges = new SelectList(challangeList,"ChallangeId","ChallangeName");
            model.ExpansionsVM = await _service.GetExpansionsByName(model.IdGame);
            var game =  _service.GetBoardgamesById(model.IdGame);
            if(game.Category=="Cooperation")
            {
                var list_pl = await _service.GetGame_PlayersById(userId);
                ViewBag.Players = new SelectList(list_pl, "HoldId", "PlayerName");


            }
            else
            {
                ViewBag.Players = new SelectList(model.PlayerVM.Where(m => m.isChecked == true), "HoldId", "PlayerName");

            }
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
                    if(item.isChecked==true)
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
            List<StatsVM> listStats2 = new List<StatsVM>();

            for (int j = 0; j < listStats.Count(); j++)
            {
                if (listStats[j].Category == "Game")
                    listStats2.Add(listStats[j]);
            }
            model.StatsVM = listStats2;

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
            await _service.AddNewMatchAsync(model);
            return RedirectToAction("Index", "Matches", new { Id = 1 });

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


        public async Task<IActionResult> Update(int Id)
        {
            var result = _service.GetById(Id);

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var dropdown = await _service.GetAllGames();
            Challange holdChallange = new Challange();
            holdChallange.ChallangeName = "Jeśli gra jest częścią kampanii, wskaż jakiej:";
            holdChallange.ChallangeId = 1;
            List<Challange> challangeList = new List<Challange>();
            List<Challange> challangeSelector = new List<Challange>();
            challangeSelector.Add(holdChallange);
            challangeList = await _service.GetChallanges();
            foreach (var item in challangeList)
            {
                if (item.ChallangeId != 1)
                    challangeSelector.Add(item);
            }
            ViewBag.Challanges = new SelectList(challangeSelector, "ChallangeId", "ChallangeName");

            ViewBag.Games = new SelectList(dropdown.Where(m => m.Expansion == false), "IdGame", "Name");
            var playerList = await _service.GetPlayersDroopDown(userId);
           // var expansionList =  _service.GetExpansions(Id);

            MatchVM match = new MatchVM();
            match.StartDate = result.StartDate.ToString();
            match.IdGame= result.IdGame;
            match.PlayerVM = playerList;
                match.MatchId= result.MatchId;
            match.ChallangeId= result.ChallangeId;
            match.UserId= userId;
            var us = _service.GetUser(userId);
            match.UserName = us.UserName;
            // match.ExpansionsVM = expansionList;

            var pl = _service.GetPlayers(Id);
            
            foreach(var item in match.PlayerVM)
            {
                for(int i=0;i<pl.Count();i++)
                {
                    if (pl[i].PlayerId == item.HoldId) item.isChecked = true;
                }
            }

            return View("Update",match);
        }


        public async Task<IActionResult> Update2(MatchVM model)
        {

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = _service.GetById(model.MatchId);

            List<Challange> challangeList = new List<Challange>();
            challangeList = await _service.GetChallanges();
            ViewBag.Challanges = new SelectList(challangeList, "ChallangeId", "ChallangeName");
            model.ExpansionsVM = await _service.GetExpansionsByName(model.IdGame);
            var game = _service.GetBoardgamesById(model.IdGame);
            if (game.Category == "Cooperation")
            {
                var list_pl = await _service.GetGame_PlayersById(userId);
                ViewBag.Players = new SelectList(list_pl, "HoldId", "PlayerName");


            }
            else
            {
                ViewBag.Players = new SelectList(model.PlayerVM.Where(m => m.isChecked == true), "HoldId", "PlayerName");

            }
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

        public async Task<IActionResult> Update3(MatchVM model)
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
                var mt_st = new StatsVM();


                mt_st.Statistic = item.Statistic;
                mt_st.StatsId = item.IdStat;
                mt_st.isChecked = true;
                mt_st.Category = item.Category;
                listStats.Add(mt_st);

            }
            listStats.Count();
          
            MatchStatVM match_st = new MatchStatVM();
            match_st.Statistic = "Test";
            List<MatchStatVM> listMatch_st = new List<MatchStatVM>();
            List<PlayerVM> listPlayer = new List<PlayerVM>();
            for (int i = 0; i < model.PlayerVM.Count(); i++)
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

            for (int i = 0; i < model.PlayerVM2.Count(); i++)
            {

                model.PlayerVM2[i].StatsVM = listPlayerStats;


            }
            List<StatsVM> listStats2 = new List<StatsVM>();

            for (int j = 0; j < listStats.Count(); j++)
            {
                if (listStats[j].Category == "Game")
                    listStats2.Add(listStats[j]);
            }
            model.StatsVM = listStats2;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update4(MatchVM data)
        {
            await _service.UpdateMatchAsync(data);
            return RedirectToAction("Index", "Matches", new { Id = 1 });

        }
    }
}
