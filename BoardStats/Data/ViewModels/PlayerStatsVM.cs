using BoardStats.Models;

namespace BoardStats.Data.ViewModels
{
    public class PlayerStatsVM
    {
        public string playerName { get; set; }
        public int howManyMatches { get; set; }
        public int howManyWins { get; set; }
        public decimal wholeTime { get; set; }
        public string mostWonGame { get; set; }

        public List<AvgStatVM> top5Games { get; set; }

    }
}
