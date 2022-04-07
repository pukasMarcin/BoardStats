using BoardStats.Data.ViewModels;
using BoardStats.Models;

namespace BoardStats.Data.Services
{
    public interface IPlayersService
    {
         Task<List<Player>> GetAllPlayersByUserId(string userId, string userRole);

        Task AddNewPlayerAsync(string userName);
        Task AddNewPlayerAsync(NewPlayerVM model);
        Player GetById(int Id);
      ApplicationUser GetUser(string userId);

        Task UpdatePlayerAsync(Player model);
        Task DeletePlayer(Player model);
    }
}
