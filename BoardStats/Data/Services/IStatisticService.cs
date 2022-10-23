
using BoardStats.Data.ViewModels;
using BoardStats.Models;

namespace BoardStats.Data.Services
{
    public interface IStatisticService
    {
        Task<StatisticsVM> GetBasicStats(string userId);
       Task<IEnumerable<Boardgames>> GetAllGames(string userId);
        Task<List<PlayerVM>> GetPlayersDroopDown(string userId);
        int CheckHistory(string userId);
      
    }
}
