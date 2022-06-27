using BoardStats.Models;

namespace BoardStats.Data.Services
{
    public interface IStatsService
    {
        Task<List<Stat>> GetAllStats();
    }
}
