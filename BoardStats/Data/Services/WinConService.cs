using BoardStats.Models;
using Microsoft.EntityFrameworkCore;

namespace BoardStats.Data.Services
{
    public class WinConService:IWinConService
    {
        public readonly ApplicationDbContext _db;
        public WinConService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<WinCon>> GetAllWinConsAsync()
        {
            var result = await _db.WinCons.ToListAsync();
            return result;
        }

        public async Task AddNewWinConAsync(WinCon model)
        {
            
            var newWinCon = new WinCon()
            {
                WinCondition = model.WinCondition,

            };

            await _db.WinCons.AddAsync(newWinCon);
            await _db.SaveChangesAsync();
        }

        public WinCon GetWinConById(int Id)
        {
            var result = _db.WinCons.FirstOrDefault(n => n.IdWinCon == Id);
            return result;
        }

        public async Task Delete(int Id)
        {
            var result = await _db.WinCons.FirstOrDefaultAsync(n => n.IdWinCon == Id);
              _db.WinCons.Remove(result);
            await _db.SaveChangesAsync();

            var result2 = await _db.Game_Wins.Where(n => n.WinConId == Id).ToListAsync();
            _db.Game_Wins.RemoveRange(result2);
            await _db.SaveChangesAsync();


        }

        public async Task Update(WinCon model)
        {
            var result = await _db.WinCons.FirstOrDefaultAsync(n => n.IdWinCon == model.IdWinCon);

            result.WinCondition = model.WinCondition;
            await _db.SaveChangesAsync();
        }
    }
}
