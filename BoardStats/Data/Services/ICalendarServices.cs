using BoardStats.Data.ViewModels;
using BoardStats.Models;

namespace BoardStats.Data.Services
{
    public interface ICalendarServices
    {
        public List<CalendarAppVM> AdminMatchesById();

        public List<CalendarAppVM> PlayerMatchesById(string Id);
        public CalendarAppVM GetById(string id);
        public Task<int> AddUpdate(CalendarAppVM model, string userId, string userName);
        public Task<int> AddUpdate2(CalendarAppVM model, string userId, string userName);
        public List<CalendarAppVM> MatchesById(string UserId);
        ApplicationUser GetUser(string userId);
        Task<IEnumerable<Boardgames>> GetAllGames();





    }
}
