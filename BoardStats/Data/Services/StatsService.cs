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

        public async Task AddNewStatAsync(Stat model)
        {
            var newStat = new Stat()
            {
                Statistic = model.Statistic,
                StatCategory = model.StatCategory,
                Category = model.Category,
                


            };
            if (newStat.Category == "1") newStat.Category = "Player";
            if (newStat.Category == "2") newStat.Category = "Game";
            if (newStat.StatCategory == "1") newStat.StatCategory = "PunktacjaSum";
            if (newStat.StatCategory == "2") newStat.StatCategory = "Zasoby";
            if (newStat.StatCategory == "3") newStat.StatCategory = "Militaria";
            if (newStat.StatCategory == "4") newStat.StatCategory = "PunktacjaPart";
            if (newStat.StatCategory == "5") newStat.StatCategory = "ToDo";
            if (newStat.StatCategory == "6") newStat.StatCategory = "ToDo2";
            if (newStat.StatCategory == "7") newStat.StatCategory = "ToDo3";
            if (newStat.StatCategory == "8") newStat.StatCategory = "ToDo4";
            if (newStat.StatCategory == "9") newStat.StatCategory = "OnStart";
            if (newStat.StatCategory == "10") newStat.StatCategory = "OnStart2";
            if (newStat.StatCategory == "11") newStat.StatCategory = "OnStart3";
            if (newStat.StatCategory == "12") newStat.StatCategory = "OnStart4";


            await _db.Stats.AddAsync(newStat);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(int Id)
        {
            var result = await _db.Stats.FirstOrDefaultAsync(n => n.IdStat == Id);
            _db.Stats.Remove(result);
            await _db.SaveChangesAsync();

            var result2 = await _db.Game_Stats.Where(n => n.StatId == Id).ToListAsync();
            _db.Game_Stats.RemoveRange(result2);
            await _db.SaveChangesAsync();
        }

        public async Task<List<Stat>> GetAllStats()
        {
            var result = await _db.Stats.ToListAsync();
            return result;
        }

        public Stat GetStatById(int Id)
        {
            var result = _db.Stats.FirstOrDefault(n => n.IdStat == Id);
            return result;
        }

        public async Task Update(Stat model)
        {
            var result = await _db.Stats.FirstOrDefaultAsync(n => n.IdStat == model.IdStat);
            result.Statistic = model.Statistic;
            result.StatCategory = model.StatCategory;
            result.Category = model.Category;

            if (result.Category == "1") result.Category = "Player";
            if (result.Category == "2") result.Category = "Game";
            if (result.StatCategory == "1") result.StatCategory = "PunktacjaSum";
            if (result.StatCategory == "2") result.StatCategory = "Zasoby";
            if (result.StatCategory == "3") result.StatCategory = "Militaria";
            if (result.StatCategory == "4") result.StatCategory = "PunktacjaPart";
            if (result.StatCategory == "5") result.StatCategory = "ToDo";
            if (result.StatCategory == "6") result.StatCategory = "ToDo2";
            if (result.StatCategory == "7") result.StatCategory = "ToDo3";
            if (result.StatCategory == "8") result.StatCategory = "ToDo4";
            if (result.StatCategory == "9") result.StatCategory = "OnStart";
            if (result.StatCategory == "10") result.StatCategory = "OnStart2";
            if (result.StatCategory == "11") result.StatCategory = "OnStart3";
            if (result.StatCategory == "12") result.StatCategory = "OnStart4";
            await _db.SaveChangesAsync();
        }
    }
}
