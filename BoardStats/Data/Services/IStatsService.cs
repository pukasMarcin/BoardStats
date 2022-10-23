using BoardStats.Models;

namespace BoardStats.Data.Services
{
    public interface IStatsService
    {
        Task<List<Stat>> GetAllStats();
        
        Task AddNewStatAsync(Stat model);

        Stat GetStatById(int Id);

        Task Delete(int Id);

        Task Update(Stat model);
    }
}
