using BoardStats.Data.ViewModels;
using BoardStats.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BoardStats.Data.Services
{
    public class GameStatService : IGameStatService
    {
        private readonly ApplicationDbContext _db;
        public GameStatService(ApplicationDbContext db)
        {
            _db = db;
        }
        public GameStatsVM GetGameStats(int Id, int Id_Player, string userId)
        {

            var game = _db.BoardGames.FirstOrDefault(m => m.IdGame == Id); //znajdz gre
            GameStatsVM result = new GameStatsVM();
            result.Game = game;
            List<Match> matches = new List<Match>(); //lista wszystkich meczy
            if(Id_Player==-1)
            {
                matches = _db.Matches.Where(m => m.IdGame == Id && m.isPlayed == true && m.UserId == userId).ToList();
            }
            else
            {
                var player_matches = _db.Match_Players.Where(m => m.PlayerId == Id_Player).ToList();
               foreach(var item in player_matches)
                {
                    var player_match = _db.Matches.FirstOrDefault(m => m.MatchId == item.MatchId && m.IdGame==game.IdGame);
                    if (player_match != null) matches.Add(player_match);
                }
                
            }
            result.playedAmount = matches.Count();
            result.avgDuration = matches.Average(m => m.Duration);
            List<Match_Player> playersList = new List<Match_Player>();
            List<AvgStatVM> avgWinCon = new List<AvgStatVM>();
            foreach (var item in matches)
            {
                var m_pl = _db.Match_Players.Where(m => m.MatchId == item.MatchId).ToList();
                playersList.AddRange(m_pl); //lista graczy grajacych w dana gre
                var winCon = _db.WinCons.FirstOrDefault(m=>m.IdWinCon==item.IdWinCon);
                AvgStatVM avgWinConStat = new AvgStatVM();
                avgWinConStat.MatchId = item.MatchId;
                avgWinConStat.ValueS = winCon.WinCondition;
                avgWinConStat.StatId = winCon.IdWinCon;
                avgWinCon.Add(avgWinConStat);//lista warunkow zwyciestwa dla danej gry
            }
            List<int> matchIds = new List<int>();//lista Id meczy
            List<int> playersAmount = new List<int>();//ile graczy bylo w danym meczu
            foreach (var item in playersList)
            {
                if (matchIds.Contains(item.MatchId) == false)
                {
                    matchIds.Add(item.MatchId);
                }
            }
            foreach (var item in matchIds)
            {
                int pl_a = playersList.Where(m => m.MatchId == item).Count();
                playersAmount.Add(pl_a);

            }

            result.avgNumberOfPlayers = System.Convert.ToInt32(System.Math.Floor(playersAmount.Average()));

            var bestWinner = (from i in matches
                              group i by i.WhoWIn into grp
                              orderby grp.Count() descending
                              select grp.First().WhoWIn
                              );

            var winningCons = (from i in avgWinCon
                               group i by i.StatId into grp
                               orderby grp.Count() descending
                               select grp.Key
                          ) ;
            result.bestWinner = bestWinner.First();

            var stats = _db.Game_Stats.Where(m => m.GameId == game.IdGame).ToList(); //lista statystyk gry
            List<AvgStatVM> match_stats = new List<AvgStatVM>();
            foreach (var item in matches)
            {
                List<Match_Stat> game_stats = new List<Match_Stat>();
                if (Id_Player == -1)
                {
                    game_stats = _db.Match_Stats.Where(m => m.MatchId == item.MatchId).ToList();
                }
                else
                {
                    game_stats = _db.Match_Stats.Where(m => m.MatchId == item.MatchId && (m.PlayerId== Id_Player || m.PlayerId ==0 || m.PlayerId ==1)).ToList();
                }
                    
                foreach (var item2 in game_stats)
                {
                    AvgStatVM avg_st1 = new AvgStatVM();
                    avg_st1.StatId = item2.IdStat;
                    avg_st1.PlayerId = item2.PlayerId;
                    avg_st1.MatchId = item2.MatchId;
                    var coreStat = _db.Stats.FirstOrDefault(m => m.IdStat == item2.IdStat);
                    if (coreStat.StatCategory == "PunktacjaSum" || coreStat.StatCategory == "PunktacjaPart")
                    {
                        avg_st1.ValueI = Int32.Parse(item2.Value);
                        avg_st1.SubCategory = coreStat.StatCategory;
                        avg_st1.Category = coreStat.Category;
                        avg_st1.Stat = coreStat.Statistic;
                    }
                    else
                    {
                        avg_st1.ValueS = item2.Value;
                        avg_st1.SubCategory = coreStat.StatCategory;
                        avg_st1.Category = coreStat.Category;
                        avg_st1.Stat = coreStat.Statistic;
                    }

                    match_stats.Add(avg_st1); //wszystkie statystyki dla meczy
                }

            }

            List<AvgStatVM> winStats = new List<AvgStatVM>();
            foreach (var item in match_stats)
            {
                if (item.SubCategory == "PunktacjaSum" || item.SubCategory=="PunktacjaPart")
                {
                    foreach(var item2 in matches)
                    {
                        int winnerId;
                        if (item2.WhoWIn == "Players")
                            winnerId = 1;
                        else winnerId = item2.IdWinner;

                        if (item.PlayerId == winnerId && item.MatchId == item2.MatchId && item2.IdWinner!=1)
                        {
                            AvgStatVM winSumPoints = new AvgStatVM();
                            decimal res = new decimal();
                                winSumPoints = match_stats.FirstOrDefault(m => m.SubCategory == "PunktacjaSum" && m.MatchId == item2.MatchId && m.PlayerId == item.PlayerId);
                            if (winSumPoints != null)
                            {
                               res = (decimal)item.ValueI / (decimal)winSumPoints.ValueI;
                                item.avgForWin = res;
                                winStats.Add(item);
                            }
                            else
                            {
                                res = (decimal)item.ValueI;
                                item.avgForWin = res;
                                winStats.Add(item);
                            }
                           
                              
                        }
                    }
                  
                }

            }
          
            var statsIds = (from i in match_stats
                             group i by i.StatId into grp
                             orderby grp.Key descending
                             select grp.Key
                             );

            var winStatsIds = (from i in winStats
                            group i by i.StatId into grp
                            orderby grp.Key descending
                            select grp.Key
                           );
            
                 List<AvgStatVM> avgWinStatsPoint = new List<AvgStatVM>();
            List<AvgStatVM> avg_stats = new List<AvgStatVM>();
            List<AvgStatVM> avg_stats_notPoints = new List<AvgStatVM>();

            foreach (var item in winStatsIds)
            {
                
                AvgStatVM avg_st_win = new AvgStatVM();
                var hold = _db.Stats.FirstOrDefault(m => m.IdStat == item);
                avg_st_win.Stat = hold.Statistic;
                avg_st_win.Category = hold.Category;
                avg_st_win.SubCategory = hold.StatCategory;
                avg_st_win.StatId = hold.IdStat;

               
                    List<decimal> values_win = new List<decimal>();
                    foreach (var item2 in winStats)
                    {
                        if (item2.StatId == item) values_win.Add(item2.avgForWin);
                    }
                avg_st_win.avgForWin = (decimal)values_win.Average();

                avgWinStatsPoint.Add(avg_st_win);
            }


                foreach (var item in statsIds)
            {
                int j = 0;
                AvgStatVM avg_st = new AvgStatVM();
                var hold = _db.Stats.FirstOrDefault(m => m.IdStat == item);
                avg_st.Stat = hold.Statistic;
                avg_st.Category = hold.Category;
                avg_st.SubCategory = hold.StatCategory;
                avg_st.StatId = hold.IdStat;
               
                if (avg_st.SubCategory == "PunktacjaSum" || avg_st.SubCategory == "PunktacjaPart")
                {
                    List<int> values = new List<int>();
                    foreach (var item2 in match_stats)
                    {
                        if (item2.StatId == item) values.Add(item2.ValueI);
                    }
                    avg_st.ValueI = System.Convert.ToInt32(System.Math.Floor(values.Average()));
                }
                else
                {
                    foreach (var item2 in match_stats)
                    {
                        if (avg_st.StatId == item2.StatId && item2.ValueS!="N/D")
                        {
                            AvgStatVM avg_st_noPoints = new AvgStatVM();
                            avg_st_noPoints.Stat = item2.ValueS;
                            avg_st_noPoints.ValueS = item2.SubCategory;
                            if (avg_stats_notPoints.Count() == 0)
                            {
                                avg_st_noPoints.ValueSAmount = 1;
                                avg_st_noPoints.ValueSAvg = 1 / result.playedAmount;
                                avg_stats_notPoints.Add(avg_st_noPoints);
                            }
                            else
                            {
                               
                                for (int i = 0; i < avg_stats_notPoints.Count(); i++)
                                {
                                    if (avg_stats_notPoints[i].Stat== avg_st_noPoints.Stat)
                                    {
                                        avg_stats_notPoints[i].ValueSAmount++;
                                        
                                        avg_stats_notPoints[i].ValueSAvg = avg_stats_notPoints[i].ValueSAmount / result.playedAmount;
                                        j++;
                                    }

                                }
                                if (j == 0)
                                {
                                    avg_st_noPoints.ValueSAmount = 1;
                                    avg_st_noPoints.ValueSAvg = 1 / result.playedAmount;
                                    avg_stats_notPoints.Add(avg_st_noPoints);

                                }
                                else if (j == 1) j--;
                            }
                            

                     
                        }


                    }



                }
                avg_stats.Add(avg_st);
            }

               
             
            

            result.avgStats = avg_stats;
                result.avgStatsNoPoints = avg_stats_notPoints;

            foreach(var item in result.avgStatsNoPoints)
            {
                item.ValueSAvg= Decimal.Divide(item.ValueSAmount, result.playedAmount);
            }

           



            List<AvgStatVM> avgForWin = new List<AvgStatVM>();
            var partPoints = result.avgStats.Where(m => m.SubCategory.Contains("PunktacjaPart")).ToList();
            var allPoints = result.avgStats.Where(m => m.SubCategory.Contains("PunktacjaSum")).ToList();
            if (partPoints.Any()) result.isPartPoints = true;
            else result.isPartPoints = false;
            if (allPoints.Any()) result.isSumPoints = true;
            else result.isSumPoints = false;

            if(result.isSumPoints==true)
            {
                var avgSumPoints = result.avgStats.Where(m => m.SubCategory == "PunktacjaSum").ToList();
                result.avgSumPoints = avgSumPoints.Average(m => m.ValueI);
            }
           

            result.avgWinStatsPoints = avgWinStatsPoint;
            List<AvgStatVM> avgWinConModel = new List<AvgStatVM>();
            foreach (var item in winningCons)
            {
                int howMany = 0;
                foreach(var item2 in matches)
                {
                    if(item==item2.IdWinCon)
                    {
                        howMany++;
                    }

                    
                }
                AvgStatVM holdWinCon = new AvgStatVM();

                var condition = _db.WinCons.FirstOrDefault(m => m.IdWinCon == item);
                holdWinCon.Stat = condition.WinCondition;
                holdWinCon.avgForWin = (decimal)howMany/(decimal)result.playedAmount;
                avgWinConModel.Add(holdWinCon);

            }
            result.avgWinCon=avgWinConModel;
            if (result.avgWinCon.Count() > 0) result.isWinCon = true;
            else result.isWinCon = false;

            decimal noPointHowMany = 0;
            foreach(var item in result.avgStatsNoPoints)
            {
                foreach(var item2 in match_stats)
                {
                    if(item.Stat==item2.ValueS)
                    {
                       var mat = _db.Matches.FirstOrDefault(m=>m.MatchId==item2.MatchId);

                        if(mat.IdWinner==item2.PlayerId || (mat.WhoWIn == "Players"))
                        {
                            noPointHowMany++;
                        }
                    }
                }
                item.avgForWin= noPointHowMany/(decimal)item.ValueSAmount;
                noPointHowMany = 0;
            }

            result.isToDo = false;
          result.isToDo2 = false;
    result.isToDo3 = false;
            result.isToDo4 = false;
            result.isOnStart = false;
            result.isOnStart2 = false;
           result.isOnStart3 = false;
       result.isOnStart4 = false;

            foreach (var item in match_stats)
            {
                if (item.SubCategory == "ToDo")
                {
                    result.isToDo = true;
                    result.ToDoName = item.Stat;
                }

                else if (item.SubCategory == "ToDo2")
                {
                    result.isToDo2 = true;
                    result.ToDoName2 = item.Stat;
                }

                else if (item.SubCategory == "ToDo3")
                {
                    result.isToDo3 = true;
                    result.ToDoName3 = item.Stat;
                }

                else if (item.SubCategory == "ToDo4")
                {
                    result.isToDo4 = true;
                    result.ToDoName4 = item.Stat;
                }

                else if (item.SubCategory == "OnStart")
                {
                    result.isOnStart = true;
                    result.OnStartName = item.Stat;
                }

                else if (item.SubCategory == "OnStart2")
                {
                    result.isOnStart2 = true;
                    result.OnStartName2 = item.Stat;
                }

                else if (item.SubCategory == "OnStart3")
                {
                    result.isOnStart3 = true;
                    result.OnStartName3 = item.Stat;
                }

                else if (item.SubCategory == "OnStart4")
                {
                    result.isOnStart4 = true;
                    result.OnStartName4 = item.Stat;
                }


            }


            var matches_pure_players = matches.Where(m => m.IdWinner != 1 && m.IdWinner != 2).ToList();
            var top5Winners = (from i in matches_pure_players
                               group i by i.IdWinner into grp
                               orderby grp.Count() descending
                               select grp.Key);

            List<AvgStatVM> topWinners = new List<AvgStatVM>();
            int counterTopPlayers = 0;
            foreach (var item in top5Winners)
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

            List<Match_Expansion> match_expansions = _db.Match_Expansions.ToList();
            List<Match_Expansion> expansions = new List<Match_Expansion>();
            foreach (var item in matches)
            {
                foreach(var item2 in match_expansions)
                {
                    if(item.MatchId == item2.MatchId)
                    {
                        var hold_exp = item2;
                        expansions.Add(hold_exp);
                    }
                }
            }
            var match_exp = (from i in expansions
                             group i by i.ExpansionId into grp
                             orderby grp.Count() descending
                             select grp.Key);

            List<AvgStatVM> playedExpansions = new List<AvgStatVM>();
            foreach(var item in match_exp)
            {
                AvgStatVM expansion = new AvgStatVM();
                var exp_id = _db.Expansions.FirstOrDefault(m => m.ExpansionId == item);
                var exp_name = _db.BoardGames.FirstOrDefault(m => m.IdGame == exp_id.IdGame);

                expansion.Stat = exp_name.Name;
                expansion.ValueI = expansions.Where(m => m.ExpansionId == item).Count();
                playedExpansions.Add(expansion);
            }
            if (playedExpansions.Count > 0) result.isExpansions = true;
            else result.isExpansions = false;
            result.playedExpansions = playedExpansions;

            List<AvgStatVM> playerCounter = new List<AvgStatVM>();
            var holdPlayers = (from i in playersList
                               group i by i.PlayerId into grp
                               orderby grp.Count() descending
                               select grp.Key
                               );


                              

            foreach(var item in holdPlayers)
            {
                int many = 0;
                foreach(var item2 in matches)
                {
                     many = _db.Match_Players.Where(m => m.PlayerId == item && m.MatchId==item2.MatchId).Count();
                }
                AvgStatVM pl_counter = new AvgStatVM();
               
                var gamer = _db.Players.FirstOrDefault(m => m.PlayerId == item);
                pl_counter.ValueS = gamer.PlayerName;
                pl_counter.ValueI = many;

                playerCounter.Add(pl_counter);
            }
            result.playersCounter = playerCounter;
            return result;

            
        }


        public async Task<List<PlayerVM>> GetPlayersDroopDown(string userId)
        {
            var result = await (from playerObj in _db.Players
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

        public HomePageVM LastGameStats(string Id)
        {
            var matches = _db.Matches.Where(m => m.UserId == Id).ToList();
            HomePageVM result = new HomePageVM();
            if(matches.Count>0)
            {
                var lastGame = matches.Where(m => m.isPlayed == true).Last();

                result.gameName = lastGame.GameName;
                result.whoWin = lastGame.WhoWIn;
                var game = _db.BoardGames.FirstOrDefault(m => m.IdGame == lastGame.IdGame);
                result.imgUrl = game.ImageUrl;
                result.duration = lastGame.Duration;

                var hold = lastGame.StartDate.ToString();
                result.when = hold.Substring(0, 10);

                var lastGames = (from i in matches
                                 orderby i.StartDate descending
                                 select i);

                List<MatchVM> last10Games = new List<MatchVM>();

                int gamesCount = 0;
                foreach (var item in lastGames)
                {
                    if (gamesCount < 4)
                    {
                        MatchVM match = new MatchVM();
                        var holdDate = item.StartDate.ToString();
                        match.StartDate = holdDate.Substring(0, 10); ;
                        match.WhoWIn = item.WhoWIn;
                        match.GameName = item.GameName;
                        last10Games.Add(match);
                        gamesCount++;
                    }


                }
                if (last10Games.Count > 0) result.last10Games = last10Games;
                return result;
            }
            else
            {
                return result;
            }
        }

        public async Task<IEnumerable<Boardgames>> GetAllGames()
        {
            var result = await _db.BoardGames.OrderBy(x => x.Name).ToListAsync();
            return result;
        }
    }
}
