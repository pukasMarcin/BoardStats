

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
    public class DetailService : IDetailService
    {
        private readonly ApplicationDbContext _db;

        public DetailService(ApplicationDbContext db)
        {
            _db = db;
        }
        public DetailVM GetMatchById(int Id)
        {
            DetailVM result = new DetailVM();
            var match =  _db.Matches.FirstOrDefault(m => m.MatchId == Id);
            result.match = match;
            var game = _db.BoardGames.FirstOrDefault(m => m.IdGame == match.IdGame);
            result.game = game;
            var expansions = _db.Match_Expansions.Where(m => m.MatchId == match.MatchId).ToList();
            List<Expansion> exp = new List<Expansion>();
           
            if (expansions.Any())
            {
                foreach(var item in expansions)
                {

                    var ex = _db.Expansions.FirstOrDefault(m => m.ExpansionId == item.ExpansionId);
                    exp.Add(ex);

                }
            }

            List<string> additions = new List<string>();
            if (exp.Any())
            {
                foreach (var item in exp)
                {
                    var name = _db.BoardGames.FirstOrDefault(m => m.IdGame == item.IdGame);
                    additions.Add(name.Name);
                }
            }
            else additions.Add("Nie wykorzystano dodatków");

            result.Expansions = additions;
            var players = _db.Match_Players.Where(m => m.MatchId == match.MatchId).ToList();
            List<Player> pl = new List<Player>();
            foreach(var item in players)
            {
                var player = _db.Players.FirstOrDefault(m => m.PlayerId == item.PlayerId);
                pl.Add(player);
            }
            result.Players = pl;
            var stats = _db.Match_Stats.Where(m => m.MatchId == match.MatchId).ToList();
            result.Match_Stats = stats;
            List<int> ints = new List<int>();
            List<Stat> sta = new List<Stat>();
            List<Stat> game_sta = new List<Stat>();
            foreach (var item in stats)
            {
                if(!ints.Contains(item.IdStat))
                {
                    ints.Add(item.IdStat);
                }
                
            }
            foreach(var item in ints)
            {
                try
                {
                    var st = _db.Stats.FirstOrDefault(m => m.IdStat == item);
                    if (st.Category == "Player")
                    {
                        sta.Add(st);
                    }
                    else if(st.Category == "Game")
                    {
                        game_sta.Add(st);
                    }
                }
                catch
                {
                    
                }
            }
            result.Stats = sta;
            result.Game_Stats = game_sta;
            var how = _db.WinCons.FirstOrDefault(m => m.IdWinCon == match.IdWinCon);
            result.How_Win = how.WinCondition;
            var hold=match.StartDate.ToString();
            result.when = hold.Substring(0, 10);
            return result;
        }
    }
}
