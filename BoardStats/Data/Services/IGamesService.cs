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
        Task<List<StatsVM>> GetNewGameDroopdownsValues2();

        Task AddNewGameAsync(BoardViewModel data);

       BoardViewModel Seeding(string id);
    }
}
