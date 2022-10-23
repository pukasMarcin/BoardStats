using BoardStats.Data.ViewModels;
using BoardStats.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BoardStats.Data.Services
{
    public class PlayerStatsService : IPlayerStatsService
    {
        private readonly ApplicationDbContext _db;
        public PlayerStatsService(ApplicationDbContext db)
        {
            _db = db;
        }
    
        public PlayerStatsVM GetPlayerStats(int Id)
        {
            PlayerStatsVM result = new PlayerStatsVM();
          
            var player = _db.Players.FirstOrDefault(m => m.PlayerId == Id);
            result.playerName = player.PlayerName;

            var playerMatches = _db.Match_Players.Where(m => m.PlayerId == Id).ToList();

            if (playerMatches.Any())
            {


                result.howManyMatches = playerMatches.Count();
                var playerWonMatches = _db.Matches.Where(m => m.IdWinner == Id).ToList();
                result.howManyWins = playerWonMatches.Count();
                var topWonGames = (from i in playerWonMatches
                                   group i by i.IdGame into grp
                                   orderby grp.Count() descending
                                   select grp.Key);
                var topGame = (from i in playerWonMatches
                               group i by i.IdGame into grp
                               orderby grp.Count() descending
                               select grp.First().GameName);

                result.mostWonGame = topGame.First();
                List<AvgStatVM> topGames = new List<AvgStatVM>();
                int countTime = 0;
                foreach (var item in playerMatches)
                {
                    Match holdMatch = new Match();
                    holdMatch = _db.Matches.FirstOrDefault(m => m.MatchId == item.MatchId);
                    countTime = countTime + holdMatch.Duration;
                }
                decimal wholeTime = countTime / 60;
                result.wholeTime = wholeTime;
                int count = 0;
                foreach (var item in topWonGames)
                {
                    if (count < 5)
                    {
                        var game = _db.BoardGames.FirstOrDefault(m => m.IdGame == item);
                        AvgStatVM holdGame = new AvgStatVM();
                        holdGame.ValueS = game.Name;
                        holdGame.ValueSAmount = playerWonMatches.Where(m => m.IdGame == item).Count();
                        topGames.Add(holdGame);
                    }
                }
                result.top5Games = topGames;
            }

            return result;

        }
    }
}
