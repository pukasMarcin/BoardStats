using BoardStats.Data.ViewModels;
using BoardStats.Models;
using Microsoft.EntityFrameworkCore;

namespace BoardStats.Data.Services
{
    public class ChallangesServices: IChallangesServices
    {
        private readonly ApplicationDbContext _db;
        public ChallangesServices(ApplicationDbContext db)
        {
            _db = db;
        }

    

        public async Task AddNewChallangeAsync(Challange model)
        {
            var user = _db.Users.FirstOrDefault(x => x.NormalizedUserName == model.UserName);

            var newChall = new Challange()
            {
               
                ChallangeName = model.ChallangeName,
                UserName = model.UserName,
                UserId = model.UserId,
            };


            await _db.Challanges.AddAsync(newChall);



            await _db.SaveChangesAsync();
        }

        public async Task DeleteChallange(Challange model)
        {
            var chall = await _db.Challanges.FirstOrDefaultAsync(m => m.ChallangeId == model.ChallangeId);

             _db.Challanges.Remove(chall);

            await _db.SaveChangesAsync();
        }

        public Challange GetById(int Id)
        {
            var challange = _db.Challanges.FirstOrDefault(m => m.ChallangeId == Id);
            return challange;
        }

        public async Task<List<Challange>> GetChallangersByUserId(string userId, string userRole)
        {
            var challanges = await _db.Challanges.Where(n => n.UserId == userId && n.ChallangeId!=1).ToListAsync();
            if (userRole == "Admin")
            {
                challanges = await _db.Challanges.ToListAsync();
            }


            return challanges;
        }

        public ApplicationUser GetUser(string userId)
        {
            ApplicationUser us = _db.Users.FirstOrDefault(x => x.Id == userId);
            return us;
        }

        public async Task UpdateChallangeAsync(Challange model)
        {
            var chall= await _db.Challanges.FirstOrDefaultAsync(n => n.ChallangeId== model.ChallangeId);

            chall.ChallangeName = model.ChallangeName;
        
            await _db.SaveChangesAsync();
        }
    }
}
