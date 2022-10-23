using BoardStats.Models;

namespace BoardStats.Data.ViewModels
{
    public class PlayedCategoriesVM
    {
        public string Category { get; set; }
        public int playedAmount { get; set; }
        public double playedShare { get; set; }
    }
}
