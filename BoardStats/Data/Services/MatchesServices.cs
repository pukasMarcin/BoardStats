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

        public async Task AddNewMatchAsync(MatchVM data)
        {
         

            if(data.ChallangeId==1)
            {
                data.isChallange = false;
                data.ChallangeId = 1;
            }
            else
            {
                data.isChallange = true;
            }
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
                ChallangeId = data.ChallangeId,
                IdWinCon = data.IdWinCon,
                IdWinner = Convert.ToInt32(data.WhoWIn),
                isPlayed=true
                



            };

            var test = NewMatch;

            await _db.Matches.AddAsync(NewMatch);

            await _db.SaveChangesAsync();



                        if (data.PlayerVM2 != null)
                        {
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
                            PlayerId = 1,
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

            if (data.ExpansionsVM != null)
            {
                for (int i = 0; i < data.ExpansionsVM.Count; i++)
                {
                    if (data.ExpansionsVM[i].isChecked == true)
                    {
                        var exp = await _db.Expansions.FirstOrDefaultAsync(n => n.IdGame == data.ExpansionsVM[i].HoldId);
                        var NewMatchExpansion = new Match_Expansion()
                        {
                            ExpansionId = exp.ExpansionId,
                            MatchId = NewMatch.MatchId
                        };
                        await _db.Match_Expansions.AddAsync(NewMatchExpansion);
                    }
                }
                await _db.SaveChangesAsync();
            }


        }

        public async Task<IEnumerable<Boardgames>> GetAllGames()
        {
            var result = await _db.BoardGames.OrderBy(x => x.Name).ToListAsync();
            return result;
        }

        public async Task<List<Match>> GetAllMatchesById(string userId, string userRole)
        {

            if (userRole == "Admin")
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

        public async Task<List<Match>> GetAllDoneMatchesById(string userId, string userRole)
        {
            List<Match> mt = new List<Match>();
            if (userRole == "Admin")
            {
                var matches = await _db.Matches.Where(m=>m.isPlayed==true).ToListAsync();
               
                    return matches;
            }
            else
            {
                var matches = await _db.Matches.Where(m => m.UserId == userId).ToListAsync();

                foreach(var item in matches)
                {
                    if (item.isPlayed == true)
                        mt.Add(item);
                }
              
                return mt;

            }


        }


        public async Task<List<Match>> GetAllMatchesByGame(string userId, string userRole, string Id)
        {
            int ID = Convert.ToInt32(Id);
            List<Match> mt = new List<Match>();
          
            if (userRole == "Admin")
            {
                var matches = await _db.Matches.Where(m => m.isPlayed == true).ToListAsync();
                foreach(var item in matches)
                {
                    if (item.IdGame == ID)
                        mt.Add(item);
                }
                return mt;
            }
            else
            {
                var matches = await _db.Matches.Where(m => m.UserId == userId).ToListAsync();
                foreach (var item in matches)
                {
                    if (item.isPlayed == true && item.IdGame==ID)
                        mt.Add(item);
                }
                return mt;

            }


        }

        public async Task<List<Match>> GetAllMatchesByChallange(string userId, string userRole, string Id)
        {
            int ID = Convert.ToInt32(Id);
            List<Match> mt = new List<Match>();

            if (userRole == "Admin")
            {
                var matches = await _db.Matches.Where(m => m.isPlayed == true).ToListAsync();
                foreach (var item in matches)
                {
                    if (item.ChallangeId == ID)
                        mt.Add(item);
                }
                return mt;
            }
            else
            {
                var matches = await _db.Matches.Where(m => m.UserId == userId).ToListAsync();
                foreach (var item in matches)
                {
                    if (item.isPlayed == true && item.ChallangeId == ID)
                        mt.Add(item);
                }
                return mt;

            }


        }

        public async Task<List<Match>> GetAllMatchesByPlayer(string userId, string userRole, string Id)
        {
            int ID = Convert.ToInt32(Id);
            List<Match> mt = new List<Match>();

            var matches = await _db.Match_Players.Where(m => m.PlayerId == ID).ToListAsync();
                foreach (var item in matches)
                {
                var match = await _db.Matches.FirstOrDefaultAsync(m => m.MatchId == item.MatchId);
                    if (match.isPlayed == true)
                        mt.Add(match);
                }
                return mt;

            


        }

        public async Task<List<Match>> GetAllMatchesByWinner(string userId, string userRole, string Id)
        {
            int ID = Convert.ToInt32(Id);
            List<Match> mt = new List<Match>();

            var matches = await _db.Match_Players.Where(m => m.PlayerId == ID).ToListAsync();
            foreach (var item in matches)
            {
                var match = await _db.Matches.FirstOrDefaultAsync(m => m.MatchId == item.MatchId);
                if (match.isPlayed == true && match.IdWinner==ID)
                    mt.Add(match);
            }
            return mt;




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
            var result = await (from playerObj in _db.Players where (playerObj.UserId==userId&&playerObj.IsActive==true&&playerObj.PlayerTag!="Game_Players")
                                select new PlayerVM()
                                {
                                    PlayerName = playerObj.PlayerName,
                                    HoldId = playerObj.PlayerId,
                                    isChecked = false
                                }
                                ).ToListAsync();

            return result;
        }

        public async Task<List<PlayerVM>> GetAllPlayersDroopDown(string userId)
        {
            var result = await (from playerObj in _db.Players
                                where (playerObj.UserId == userId)
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

        public Match GetMatchById(int Id)
        {
            var match = _db.Matches
                
                .Include(wg => wg.Match_Player).ThenInclude(w => w.Player)
                .FirstOrDefault(m => m.MatchId == Id);

            

            return match;
        }

        public List< Match_Player> GetPlayers(int Id)
        {
            List<Match_Player> newList = new List<Match_Player>();
            newList =  _db.Match_Players.Where(m => m.MatchId == Id).ToList();
            return newList;
        }

        public List<ExpansionVM> GetExpansions(int Id)
        {
            List<Match_Expansion> newList = new List<Match_Expansion>();
            newList = _db.Match_Expansions.Where(m => m.MatchId == Id).ToList();
            List<ExpansionVM> expList = new List<ExpansionVM>();
            var hold = new ExpansionVM();

            foreach(var item in newList)
            {
                var h = _db.Expansions.FirstOrDefault(m => m.ExpansionId == item.ExpansionId);

                var g = _db.BoardGames.FirstOrDefault(n => n.IdGame == h.IdGame);
                hold.isChecked = true;
                hold.ExpansionName = g.Name;
                hold.HoldId = g.IdGame;
                expList.Add(hold);

            }
            return expList;
        }

        public async Task UpdateMatchAsync(MatchVM data)
        {
            if (data.ChallangeId == 1)
            {
                data.isChallange = false;
                data.ChallangeId = 1;
            }
            else
            {
                data.isChallange = true;
            }
         
            var gamers = await (from pl in _db.Players
                                where pl.UserId == data.UserId
                                select new PlayerVM()
                                {
                                    HoldId = pl.PlayerId,
                                    PlayerName = pl.PlayerName,
                                    isChecked = pl.IsActive
                                }
            ).ToListAsync();

            var gamer = gamers.FirstOrDefault(m => m.HoldId == Convert.ToInt32(data.WhoWIn));

            var oldMatch = await _db.Matches.FirstOrDefaultAsync(m => m.MatchId == data.MatchId);
            var gameName = await _db.BoardGames.FirstOrDefaultAsync(m=>m.IdGame == data.IdGame);
            oldMatch.IdGame = data.IdGame;
            oldMatch.GameName = gameName.Name; 
           try
            {
                oldMatch.StartDate = DateTime.ParseExact(data.StartDate, "MM/dd/yyyy", null);
            }
            catch
            {
                oldMatch.StartDate = DateTime.ParseExact(data.StartDate.Substring(0, 10), "dd/MM/yyyy", null);
            }
               
            oldMatch.Duration = data.Duration;
            oldMatch.UserName = data.UserName;
            oldMatch.UserId = data.UserId;
            oldMatch.WhoWIn = gamer.PlayerName;
            oldMatch.isChallange = data.isChallange;
            oldMatch.ChallangeId = data.ChallangeId;
            oldMatch.IdWinCon = data.IdWinCon;
            oldMatch.IdWinner = Convert.ToInt32(data.WhoWIn);
            oldMatch.isPlayed = true;

            await _db.SaveChangesAsync();

           
            var statsList = await _db.Match_Stats.Where(m => m.MatchId == data.MatchId).ToListAsync();
            if (statsList != null) _db.Match_Stats.RemoveRange(statsList);

            if(data.PlayerVM2!=null)
            {
                for (int j = 0; j < data.PlayerVM2.Count; j++)
                {
                    if (data.PlayerVM2[j].StatsVM != null)
                    {
                        for (int i = 0; i < data.PlayerVM2[j].StatsVM.Count; i++)
                        {
                            var MatchStat = new Match_Stat()
                            {
                                MatchId = data.MatchId,
                                PlayerId = data.PlayerVM2[j].HoldId,
                                IdStat = data.PlayerVM2[j].StatsVM[i].StatsId,
                                Value = data.PlayerVM2[j].StatsVM[i].Value,
                            };
                            await _db.Match_Stats.AddAsync(MatchStat);
                        }
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
                            MatchId = data.MatchId,
                            PlayerId = 1,
                            IdStat = data.StatsVM[i].StatsId,
                            Value = data.StatsVM[i].Value,
                        };
                        await _db.Match_Stats.AddAsync(MatchStat);
                    }
                }
            }
            await _db.SaveChangesAsync();

            var playerList = await _db.Match_Players.Where(m => m.MatchId == data.MatchId).ToListAsync();
            if (playerList != null) _db.Match_Players.RemoveRange(playerList);
            for (int i = 0; i < data.PlayerVM2.Count; i++)
            {
                var NewMatchPlayer = new Match_Player()
                {
                    PlayerId = data.PlayerVM2[i].HoldId,
                    MatchId = data.MatchId
                };
                await _db.Match_Players.AddAsync(NewMatchPlayer);
            }
            await _db.SaveChangesAsync();

            var expList = await _db.Match_Expansions.Where(m => m.MatchId == data.MatchId).ToListAsync();
            _db.Match_Expansions.RemoveRange(expList);

            if (data.ExpansionsVM != null)
            {
                for (int i = 0; i < data.ExpansionsVM.Count; i++)
                {
                    if (data.ExpansionsVM[i].isChecked == true)
                    {
                        var exp = await _db.Expansions.FirstOrDefaultAsync(n => n.IdGame == data.ExpansionsVM[i].HoldId);
                        var NewMatchExpansion = new Match_Expansion()
                        {
                            ExpansionId = exp.ExpansionId,
                            MatchId = data.MatchId
                        };
                        await _db.Match_Expansions.AddAsync(NewMatchExpansion);
                    }
                      
                }
            }
            await _db.SaveChangesAsync();
        }

        public Boardgames GetBoardgamesById(int Id)
        {
            var result = _db.BoardGames.FirstOrDefault(m => m.IdGame == Id);
            return result;
        }

        public async Task<List<PlayerVM>> GetGame_PlayersById(string Id)
        {
            var result = await(from playerObj in _db.Players
                               where (playerObj.UserId == Id && playerObj.PlayerTag == "Game_Players")
                               select new PlayerVM()
                               {
                                   PlayerName = playerObj.PlayerName,
                                   HoldId = playerObj.PlayerId,
                                   isChecked = true
                               }
                                ).ToListAsync();
            return result;
        }

        public async Task<List<Challange>> GetChallanges(string userId)
        {
            var result = await _db.Challanges.Where(m=>m.UserId==userId && m.ChallangeId!=1).ToListAsync();
            return result;
        }

    }
}
