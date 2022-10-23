using BoardStats.Data.ViewModels;
using BoardStats.Models;
namespace BoardStats.Data.Services
{
    public interface IGameStatService
    {
        GameStatsVM GetGameStats(int Id, int Id_Player, string userId);
        Task<List<PlayerVM>> GetPlayersDroopDown(string userId);

        HomePageVM LastGameStats(string Id);
        Task<IEnumerable<Boardgames>> GetAllGames();
    }
}
