using BoardStats.Data.ViewModels;
using BoardStats.Models;


namespace BoardStats.Data.Services
{
    public interface IGamesService
    {

        Task<IEnumerable<Boardgames>> GetAll();
      Boardgames GetById(int id);
        public void Add(Boardgames game);
        Boardgames Update(int id, Boardgames newGame);
        void Delete(int id);
        Task<NewGameVM> GetNewGameDroopdownsValues();
        Task<List<StatsVM>> GetNewGameDroopdownStats();

       

        Task<List<WinVM>> GetNewGameDroopdownWins();

        
        Task AddNewGameAsync(BoardViewModel data);

        Task UpdateGameAsync(BoardViewModel data);

        Task DeleteGameAsync(int Id);

        BoardViewModel Seeding(string id);
    }
}
