using BoardStats.Data.ViewModels;
using BoardStats.Models;
using System.Globalization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace BoardStats.Data.Services
{
    public class CalendarServices : ICalendarServices
    {
        public readonly ApplicationDbContext _db;

        public CalendarServices(ApplicationDbContext db)
        {
            _db = db;
        }
        public List<CalendarAppVM> AdminMatchesById()
        {
            return _db.Matches.ToList().Select(c => new CalendarAppVM()
            {
                StartDate = c.StartDate.ToString(),
                GameName = c.GameName,
                IdGame = c.IdGame,

            }).ToList();
        }

        public CalendarAppVM GetById(string id)
        {

            var result = _db.Matches.FirstOrDefault(m => m.MatchId == Int32.Parse(id));
            CalendarAppVM model = new CalendarAppVM()
            {
                IdGame = result.IdGame,
                StartDate = result.StartDate.ToString(),
                MatchId = result.MatchId.ToString(),
                GameName = result.GameName,
            };

            return model;
        }

        public List<CalendarAppVM> PlayerMatchesById(string Id)
        {
            return _db.Matches.Where(m => m.UserId == Id).ToList().Select(c => new CalendarAppVM()
            {
                StartDate = c.StartDate.ToString(),
                GameName = c.GameName,
                IdGame = c.IdGame,
            }).ToList();
        }

        public async Task<int> AddUpdate(CalendarAppVM model, string userId, string userName)
        {
            var startDate = model.StartDate;
            var idGame = model.IdGame;
            var matchId = model.MatchId;
            var gameName = model.GameName;


            if (model != null && Int32.Parse(model.MatchId) > 0)
            {
                //update logic
                return 1;
            }
            else
            {
                //create logic

                var game = await _db.BoardGames.FirstOrDefaultAsync(m => m.Name == gameName);
                Match match = new Match()
                {
                    IdGame = game.IdGame,
                    GameName = gameName,
             
                StartDate = DateTime.Parse(startDate),
                Duration = 0,
                    UserName = userName,
                    UserId = userId,
                    WhoWIn = "Brak",
                    isChallange = false,
                    ChallangeId = 1,
                    IdWinCon = 1,
                    IdWinner = 1,
                    isPlayed = false,
                };

                

                await _db.Matches.AddAsync(match);

                await _db.SaveChangesAsync();
                return 2;

            }
        }

        public async Task<int> AddUpdate2(CalendarAppVM model, string userId, string userName)
        {
            var startDate = model.StartDate;
            var idGame = model.IdGame;
            var matchId = model.MatchId;
            var gameName = model.GameName;


            if (model != null && Int32.Parse(model.MatchId) > 0)
            {
                //update logic
                return 1;
            }
            else
            {
                //create logic
                DateTime date;
                try
                {
                    date = DateTime.Parse(startDate);
                }
                catch(Exception e)
                {
                   
                    string stDate = startDate.Substring(0, 10);
                    date = DateTime.ParseExact(stDate, "MM/dd/yyyy", null);
                  
                }
                var game = await _db.BoardGames.FirstOrDefaultAsync(m => m.Name == gameName);
                Match match = new Match()
                {
                    IdGame = game.IdGame,
                    GameName = gameName,

                  
                    StartDate = date,
                    Duration = 0,
                    UserName = userName,
                    UserId = userId,
                    WhoWIn = "Brak",
                    isChallange = false,
                    ChallangeId = 1,
                    IdWinCon = 1,
                    IdWinner = 1,
                    isPlayed = false,
                };



                await _db.Matches.AddAsync(match);

                await _db.SaveChangesAsync();
                return 2;

            }
        }
        public List<CalendarAppVM> MatchesById(string UserId)
        {

            return _db.Matches.Where(x => x.UserId == UserId).ToList().Select(c => new CalendarAppVM()
            {

                StartDate = c.StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
                IdGame = c.IdGame,
                MatchId = c.MatchId.ToString(),
                GameName = c.GameName,
                isPlayed = c.isPlayed

            }).ToList();


        }




        public ApplicationUser GetUser(string userId)
        {

            ApplicationUser us = _db.Users.FirstOrDefault(x => x.Id == userId);
            return us;

        }

        public async Task<IEnumerable<Boardgames>> GetAllGames()
        {
            var result = await _db.BoardGames.OrderBy(x => x.Name).ToListAsync();
            return result;
        }
    }
}
