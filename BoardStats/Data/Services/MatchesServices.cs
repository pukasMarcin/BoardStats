using BoardStats.Data.ViewModels;
using BoardStats.Models;
using Microsoft.EntityFrameworkCore;


namespace BoardStats.Data.Services
{
    public class MatchesServices : IMatchesServices
    {
        public readonly ApplicationDbContext _db;
        public MatchesServices(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task AddNewMatch(MatchVM data)
        {
            int chall=0;
            if (data.isChallange == false) chall = 1;
            else chall = data.ChallangeId;
            var gamers = await (from pl in _db.Players
                         where pl.UserId == data.UserId
                         select new PlayerVM()
                         {
                             HoldId=pl.PlayerId,
                             PlayerName=pl.PlayerName,
                             isChecked=pl.IsActive
                         }
            ).ToListAsync();

            var gamer = gamers.FirstOrDefault(m => m.HoldId == Convert.ToInt32(data.WhoWIn));

            
            DateTime hold = DateTime.ParseExact(data.StartDate, "MM/dd/yyyy", null);

            var NewMatch = new Match()
            {



                IdGame = data.IdGame,
                GameName = data.GameName,
                StartDate = hold,

                Duration = data.Duration,
                UserName = data.UserName,
                UserId = data.UserId,
                
                WhoWIn = gamer.PlayerName,
                isChallange = data.isChallange,
                ChallangeId = chall,
                IdWinCon = data.IdWinCon,
                IdWinner = Convert.ToInt32(data.WhoWIn),
          
                



            };

            var test = NewMatch;

            await _db.Matches.AddAsync(NewMatch);

            await _db.SaveChangesAsync();
           
                    for (int j = 0; j < data.PlayerVM2.Count; j++)
                    {
                if (data.PlayerVM2[j].StatsVM != null)
                {
                    for (int i = 0; i < data.PlayerVM2[j].StatsVM.Count; i++)
                    {
                        var MatchStat = new Match_Stat()
                        {
                            MatchId = NewMatch.MatchId,
                            PlayerId = data.PlayerVM2[j].HoldId,
                            IdStat = data.PlayerVM2[j].StatsVM[i].StatsId,
                            Value = data.PlayerVM2[j].StatsVM[i].Value,
                        };
                        await _db.Match_Stats.AddAsync(MatchStat);
                    }
                }

                    }
            await _db.SaveChangesAsync();

            if (data.StatsVM != null)
            {
                for (int i = 0; i < data.StatsVM.Count; i++)
                {
                    if (data.StatsVM[i].Category == "Game")
                    {
                        var MatchStat = new Match_Stat()
                        {
                            MatchId = NewMatch.MatchId,
                            PlayerId = -1,
                            IdStat = data.StatsVM[i].StatsId,
                            Value = data.StatsVM[i].Value,
                        };
                        await _db.Match_Stats.AddAsync(MatchStat);
                    }
                }
            }
            await _db.SaveChangesAsync();


            for (int i = 0; i < data.PlayerVM2.Count; i++)
            {
                var NewMatchPlayer = new Match_Player()
                {
                    PlayerId = data.PlayerVM2[i].HoldId,
                    MatchId = NewMatch.MatchId
                };
                await _db.Match_Players.AddAsync(NewMatchPlayer);
            }
            await _db.SaveChangesAsync();



        }

        public async Task<IEnumerable<Boardgames>> GetAllGames()
        {
            var result = await _db.BoardGames.OrderBy(x => x.Name).ToListAsync();
            return result;
        }

        public async Task<List<Match>> GetAllMatchesById(string userId, string userRole)
        {
            
            if(userRole=="Admin")
            { 
                var matches = await _db.Matches.ToListAsync();
                return matches;
            }
            else
            {
               var matches = await _db.Matches.Where(m => m.UserId == userId).ToListAsync();
                return matches;

            }


        }

        public async Task<List<Challange>> GetChallanges()
        {
            var result = await _db.Challanges.ToListAsync();
            return result;
        }

        public async Task<List<ExpansionVM>> GetExpansionsByName(int Id)
        {
            //var result = await _db.BoardGames.Where(m => m.Name == name).ToListAsync();

            var game = await _db.BoardGames.FirstOrDefaultAsync(m => m.IdGame == Id);
            var result = await (from expObj in _db.BoardGames
                                where (expObj.MainGame == game.Name && expObj.Name!=game.Name)
                                select new ExpansionVM()
                                {
                                    HoldId=expObj.IdGame,
                                    ExpansionName = expObj.Name,
                                    isChecked = false
                                }
                                ).ToListAsync();
            return result;
        }

        public string GetGameById(int Id)
        {
            var game =  _db.BoardGames.FirstOrDefault(m => m.IdGame == Id);
            return game.Name;

        }

        public async Task<List<PlayerVM>> GetPlayersDroopDown(string userId)
        {
            var result = await (from playerObj in _db.Players where (playerObj.UserId==userId&&playerObj.IsActive==true)
                                select new PlayerVM()
                                {
                                    PlayerName = playerObj.PlayerName,
                                    HoldId = playerObj.PlayerId,
                                    isChecked = false
                                }
                                ).ToListAsync();

            return result;
        }

        public async Task<List<Stat>> GetStats(List<int> list)
        {
            List<Stat> result = new List<Stat>();
            List<int> stats = new List<int>();
            foreach(var item in list)
            {
                var stat = await _db.Game_Stats.Where(m => m.GameId == item).ToListAsync();
                foreach(var item2 in stat)
                {
                    if (stats.Contains(item2.StatId) == false)
                    {

                        stats.Add(item2.StatId);
                    }
                            
                }
            }

            foreach(var item in stats)
            {
                var res = await _db.Stats.FirstOrDefaultAsync(m => m.IdStat == item);
                result.Add(res);
            }
            return result;
        }

        public async Task<List<WinCon>> GetWins(List<int> list)
        {
            List<WinCon> result = new List<WinCon>();
            List<int> wins = new List<int>();
            foreach (var item in list)
            {
                var win = await _db.Game_Wins.Where(m => m.GameId == item).ToListAsync();
                foreach (var item2 in win)
                {
                    if (wins.Contains(item2.WinConId) == false)
                    {

                       wins.Add(item2.WinConId);
                    }

                }
            }

            foreach (var item in wins)
            {
                var res = await _db.WinCons.FirstOrDefaultAsync(m => m.IdWinCon == item);
                result.Add(res);
            }
            return result;
        }

        public ApplicationUser GetUser(string userId)
        {
            ApplicationUser us = _db.Users.FirstOrDefault(x => x.Id == userId);
            return us;
        }
        public Match GetById(int Id)
        {
            var match = _db.Matches.FirstOrDefault(m => m.MatchId == Id);
            return match;
        }

        public async Task DeleteMatch(Match model)
        {
            var result = await _db.Matches.FirstOrDefaultAsync(n => n.MatchId == model.MatchId);
            _db.Matches.Remove(result);
            await _db.SaveChangesAsync();

            var result2 = await _db.Match_Stats.Where(n => n.MatchId == model.MatchId).ToListAsync();
            _db.Match_Stats.RemoveRange(result2);
            await _db.SaveChangesAsync();

            var result3 = await _db.Match_Players.Where(n => n.MatchId == model.MatchId).ToListAsync();
            _db.Match_Players.RemoveRange(result3);
            await _db.SaveChangesAsync();

        }
    }
}
