
using BoardStats.Models;
namespace BoardStats.Data.ViewModels
{
    public class PlayersPlayedVM
    {
        public string PlayerName { get; set; }
        public int PlayerId { get; set; }
        public int matchesPlayed { get; set; }
        public int matchesShare { get; set; }
        public int winAmount { get; set; }
        public int winShare { get; set; }
    }
}
