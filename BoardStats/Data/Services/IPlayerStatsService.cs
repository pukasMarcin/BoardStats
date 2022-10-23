using BoardStats.Data.ViewModels;
using BoardStats.Models;

namespace BoardStats.Data.Services
{
    public interface IPlayerStatsService
    {
        PlayerStatsVM GetPlayerStats(int Id);
    }
}
