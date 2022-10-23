using BoardStats.Data.ViewModels;
using BoardStats.Models;

namespace BoardStats.Data.Services
{
    public interface IMatchesServices
    {
        Task<List<Match>>GetAllMatchesById(string userId, string userRole);
        Task<List<Match>> GetAllMatchesByGame(string userId, string userRole, string Id);

        Task<List<Match>> GetAllMatchesByChallange(string userId, string userRole, string Id);
        Task<List<Match>> GetAllMatchesByPlayer(string userId, string userRole, string Id);

        Task<List<Match>> GetAllMatchesByWinner(string userId, string userRole, string Id);
        Task<List<Match>> GetAllDoneMatchesById(string userId, string userRole);
        Task<IEnumerable<Boardgames>> GetAllGames();

        Task<List<PlayerVM>> GetPlayersDroopDown(string userId);
        Task<List<PlayerVM>> GetAllPlayersDroopDown(string userId);
        Task<List<ExpansionVM>> GetExpansionsByName(int Id);
        Task<List<Challange>> GetChallanges(string userId);

        Task<List<Stat>> GetStats(List<int>list);
        Task<List<WinCon>> GetWins(List<int> list);
        Task<List<Challange>> GetChallanges();
        Task AddNewMatchAsync(MatchVM match);

         string GetGameById(int Id);
        ApplicationUser GetUser(string userId);
        Match GetById(int Id);
        Task DeleteMatch(Match model);
        Match GetMatchById(int Id);
        List<Match_Player> GetPlayers(int Id);
        List<ExpansionVM> GetExpansions(int Id);
        Boardgames GetBoardgamesById(int Id);
       Task< List<PlayerVM>> GetGame_PlayersById(string Id);
        Task UpdateMatchAsync(MatchVM data);

    }
}
