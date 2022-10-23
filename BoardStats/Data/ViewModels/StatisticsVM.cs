using BoardStats.Models;
using BoardStats.Data.ViewModels;

namespace BoardStats.Data.ViewModels
{
    public class StatisticsVM
    {
        public int matchAmount { get; set; }
        public List<PlayedCategoriesVM> playedCategories { get; set; }
        public List<Top5GamesVM> topGames { get; set; }
        public string bestWinner { get; set; }
        public List<PlayersPlayedVM> playedPlayers { get; set; }

        public List<AvgStatVM> top5Winners { get; set; }
        public string mostPlayedGame { get; set; }
        public double avgPlayersNumber { get; set; }

    }
}
