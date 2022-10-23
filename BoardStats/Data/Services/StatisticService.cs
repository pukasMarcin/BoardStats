

using BoardStats.Data.ViewModels;
using BoardStats.Models;
using BoardStats.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Xml;

namespace BoardStats.Data.Services
{
    public class StatisticService: IStatisticService
    {
        private readonly ApplicationDbContext _db;
        public StatisticService(ApplicationDbContext db )
        {
            _db = db;
        }

        public int CheckHistory(string userId)
        {
            var check = _db.Matches.Where(m => m.UserId == userId).ToList();
            if (check.Count > 0) return 1;
            else return 0;
        }

        public async Task<IEnumerable<Boardgames>> GetAllGames(string userId)
        {
            List<Boardgames> boardgames = new List<Boardgames>();
            var result =  await _db.BoardGames.OrderBy(x => x.Name).ToListAsync();
            foreach(var item in result)
            {
                var match = _db.Matches.FirstOrDefault(m => m.IdGame == item.IdGame && m.UserId==userId);
                if (match != null) boardgames.Add(item);
            }
            return boardgames;
        }

        public async Task<StatisticsVM> GetBasicStats(string userId)
        {
            StatisticsVM result = new StatisticsVM();

            var players_list = await _db.Players.Where(m => m.UserId == userId).ToListAsync();
            List<Match_Player> match_players = new List<Match_Player>();
            foreach(var item in players_list)
            {
                var hold_players = await _db.Match_Players.Where(m => m.PlayerId == item.PlayerId).ToListAsync();
                match_players.AddRange(hold_players);
            }
            
            var matches = await _db.Matches.Where(m=>m.isPlayed==true && m.UserId==userId).ToListAsync();
            List<Boardgames> playedGame = new List<Boardgames>();//lista rozegranych gier
            result.matchAmount=matches.Count; //ile gier!!!
            List<string> categories = new List<string>();
            foreach(var item in matches)
            {
                var game =  await _db.BoardGames.FirstOrDefaultAsync(m => m.IdGame == item.IdGame);
                playedGame.Add(game);   
                if(categories.Contains(game.Category)==false)
                {
                    categories.Add(game.Category);
                }
            }
            List<PlayedCategoriesVM> playedCategories = new List<PlayedCategoriesVM>();
            foreach(var item in categories)
            {
                var hold = playedGame.Where(m => m.Category == item).ToList();

                PlayedCategoriesVM plCat = new PlayedCategoriesVM()
                {
                    Category = item,
                    playedAmount = hold.Count(),
                    playedShare = (hold.Count() / playedGame.Count())
                };
                playedCategories.Add(plCat);//statystyki kategorii
            }

            result.playedCategories = playedCategories;


            var most =        (from i in playedGame
                              group i by i.Name into grp
                              orderby grp.Count() descending
                              select grp.First().Name);

            var mostGamesCount = (from i in matches
                             group i by i.IdGame into grp
                             orderby grp.Count() descending
                             select grp.Count());

            var mostGamesID = (from i in matches
                                  group i by i.IdGame into grp
                                  orderby grp.Count() descending
                                  select grp.Key);
            List<string> mostGamesNames = new List<string>();
            foreach(var item in mostGamesID)
            {
                var hold = await _db.BoardGames.FirstOrDefaultAsync(m => m.IdGame == item);
                string hold_s = hold.Name;
                mostGamesNames.Add(hold_s);
            }
            List<int> mostGamesIds = new List<int>();
            foreach (var item in mostGamesCount)
            {
                int i = item;
                mostGamesIds.Add(item);
            }

            result.mostPlayedGame = most.First();

            List<Top5GamesVM> topGames = new List<Top5GamesVM>();
           for (int i = 0;i<mostGamesNames.Count();i++)
            {
                for(int j=0;j<mostGamesIds.Count();j++)
                {
                    if (i < 6 && i==j)
                    {
                        Top5GamesVM topGame = new Top5GamesVM();
                        topGame.Name = mostGamesNames[i];
                        topGame.playedAmount = mostGamesIds[j];
                        topGames.Add(topGame);   
                    }

                }
                if (i == 6) i = mostGamesNames.Count();



             }
               
            
            var mostWinner = (from i in matches
                        group i by i.WhoWIn into grp
                        orderby grp.Count() descending
                        select grp.First().WhoWIn);

            result.bestWinner = mostWinner.First();
            result.topGames = topGames;

            List<int> avgPlayers = new List<int>();
            foreach(var item in matches)
            {
                var players = await _db.Match_Players.Where(m=>m.MatchId == item.MatchId).ToListAsync();
                int i = players.Count();
                avgPlayers.Add(i);
            }
           
            result.avgPlayersNumber = System.Convert.ToInt32(System.Math.Floor(avgPlayers.Average()));
            List<PlayersPlayedVM> playedPl = new List<PlayersPlayedVM>();
            var mostPlayedPlayersA = (from i in match_players
                                     group i by i.PlayerId into grp
                                     orderby grp.Count() descending
                                     select grp.Count()) ;
            var mostPlayedPlayersId = (from i in match_players
                                      group i by i.PlayerId into grp
                                      orderby grp.Count() descending
                                      select grp.Key);

            List<string> playersName = new List<string>();
            List<int> playersAmount = new List<int>();
            foreach (var item in mostPlayedPlayersId)
            {
                var h =  await _db.Players.FirstOrDefaultAsync(m => m.PlayerId == item);
                string n = h.PlayerName;
                playersName.Add(n);
            }
            foreach (var item in mostPlayedPlayersA)
            {
                
                playersAmount.Add(item);
            }
            for (int i=0;i< playersName.Count();i++)
            {
                for(int j=0;j< playersAmount.Count();j++)
                {
                    if(i==j)
                    {
                        PlayersPlayedVM hold_p = new PlayersPlayedVM();
                        hold_p.PlayerName= playersName[i];
                        hold_p.matchesPlayed = playersAmount[j];
                        playedPl.Add(hold_p);
                    }
                }
            }
            result.playedPlayers = playedPl;

            var matches_pure_players = matches.Where(m => m.IdWinner != 1 && m.IdWinner != 2).ToList();
            var top5Winners = (from i in matches_pure_players
                               group i by i.IdWinner into grp
                               orderby grp.Count() descending
                               select grp.Key);

            List<AvgStatVM> topWinners = new List<AvgStatVM>();
            int counterTopPlayers = 0;
          foreach(var item in top5Winners)
            {
                if (counterTopPlayers < 5)
                {
                    AvgStatVM holdTopWinners = new AvgStatVM();
                    Player holdPlayer = new Player();
                    holdPlayer = _db.Players.FirstOrDefault(m => m.PlayerId == item);

                    holdTopWinners.Stat = holdPlayer.PlayerName;
                    int numberOfWins = matches.Where(m => m.IdWinner == item).ToList().Count();
                    holdTopWinners.ValueI = numberOfWins;
                    topWinners.Add(holdTopWinners);
                    counterTopPlayers++;
                }
            }
            result.top5Winners = topWinners;
            return result;

        }

        

        public async Task<List<PlayerVM>> GetPlayersDroopDown(string userId)
        {
            var result = await(from playerObj in _db.Players
                               where (playerObj.UserId == userId && playerObj.IsActive == true && playerObj.PlayerTag != "Game_Players")
                               select new PlayerVM()
                               {
                                   PlayerName = playerObj.PlayerName,
                                   HoldId = playerObj.PlayerId,
                                   isChecked = false
                               }
                                 ).ToListAsync();

            

            return result;
        }
    }
}
