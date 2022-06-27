using BoardStats.Data.ViewModels;
using BoardStats.Models;

namespace BoardStats.Data.Services
{
    public interface IMatchesServices
    {
        Task<List<Match>>GetAllMatchesById(string userId, string userRole);
        Task<IEnumerable<Boardgames>> GetAllGames();

        Task<List<PlayerVM>> GetPlayersDroopDown(string userId);

        Task<List<ExpansionVM>> GetExpansionsByName(int Id);

        Task<List<Stat>> GetStats(List<int>list);
        Task<List<WinCon>> GetWins(List<int> list);
        Task<List<Challange>> GetChallanges();
        Task AddNewMatch(MatchVM match);

         string GetGameById(int Id);
        ApplicationUser GetUser(string userId);
        Match GetById(int Id);
        Task DeleteMatch(Match model);


    }
}
