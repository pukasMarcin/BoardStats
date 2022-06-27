using BoardStats.Models;
using Microsoft.EntityFrameworkCore;

namespace BoardStats.Data.Services
{
    public class StatsService : IStatsService
    {
        public readonly ApplicationDbContext _db;

        public StatsService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<Stat>> GetAllStats()
        {
            var result = await _db.Stats.ToListAsync();
            return result;
        }
    }
}
