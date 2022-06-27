using BoardStats.Models;

namespace BoardStats.Data.Services
{
    public interface IWinConService
    {
        Task<List<WinCon>> GetAllWinConsAsync();
        Task AddNewWinConAsync(WinCon model);

        WinCon GetWinConById(int Id);

        Task Delete(int Id);

        Task Update(WinCon model);

    }
}
