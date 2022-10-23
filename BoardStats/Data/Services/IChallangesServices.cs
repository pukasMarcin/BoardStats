using BoardStats.Data.ViewModels;
using BoardStats.Models;

namespace BoardStats.Data.Services
{
    public interface IChallangesServices
    {
        Task<List<Challange>> GetChallangersByUserId(string userId, string userRole);

  
        Task AddNewChallangeAsync(Challange model);
        Challange GetById(int Id);
        ApplicationUser GetUser(string userId);

        Task UpdateChallangeAsync(Challange model);
        Task DeleteChallange(Challange model);
    }
}
