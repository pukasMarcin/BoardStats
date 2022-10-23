

using BoardStats.Data.ViewModels;
using BoardStats.Models;

namespace BoardStats.Data.Services
{
    public interface IDetailService
    {
        DetailVM GetMatchById(int Id);

    }
}
