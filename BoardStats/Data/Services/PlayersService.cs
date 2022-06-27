using BoardStats.Data.ViewModels;
using BoardStats.Models;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace BoardStats.Data.Services
{
    public class PlayersService : IPlayersService
    {

        public readonly ApplicationDbContext _db;

        public PlayersService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task AddNewPlayerAsync(string userName)
        {
            var user = _db.Users.FirstOrDefault(x => x.UserName == userName);
            var newPlayer = new Player()
            {
                PlayerName = userName,
                PlayerTag = "Player",
                UserId = user.Id,
                UserName=userName,
                IsActive = true,

            };

           await _db.Players.AddAsync(newPlayer);
           await _db.SaveChangesAsync();
        }

        public async Task AddNewPlayerAsync(NewPlayerVM model)
        {
            var user = _db.Users.FirstOrDefault(x => x.NormalizedUserName == model.UserName);
            var newPlayer = new Player()
            {
                PlayerName = model.PlayerName,
                PlayerTag = model.PlayerTag,
                UserId = user.Id,
                UserName = model.UserName,
                IsActive = true
            };

            await _db.Players.AddAsync(newPlayer);
           await _db.SaveChangesAsync();
        }

        public async Task DeletePlayer(Player model)
        {
            var player = await _db.Players.FirstOrDefaultAsync(m => m.PlayerId == model.PlayerId);

      
                player.IsActive = false;
                
            
           
            await _db.SaveChangesAsync();

        }

        public async Task<List<Player>> GetAllPlayersByUserId(string userId, string userRole)
        {

            var players = await _db.Players.Where(n => n.UserId == userId).ToListAsync();
            if (userRole == "Admin")
            {
                players = await _db.Players.ToListAsync();
            }
           

            return players;

        }

        public Player GetById(int Id)
        {
            var player = _db.Players.FirstOrDefault(m => m.PlayerId==Id);
            return player;
        }

        public ApplicationUser GetUser(string userId)
        {
            ApplicationUser us =  _db.Users.FirstOrDefault(x => x.Id == userId);
            return us;
        }

        public async Task UpdatePlayerAsync(Player model)
        {
            var player = await _db.Players.FirstOrDefaultAsync(n => n.PlayerId == model.PlayerId);

            player.UserId = model.UserId;
            player.UserName = model.UserName;
            player.PlayerTag = model.PlayerTag;
            player.PlayerName=model.PlayerName;
            player.IsActive = model.IsActive;
            await _db.SaveChangesAsync();

        }
    }
}
