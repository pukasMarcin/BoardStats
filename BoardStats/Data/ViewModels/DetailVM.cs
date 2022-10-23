using BoardStats.Models;

namespace BoardStats.Data.ViewModels
{
    public class DetailVM
    {
        public Match match { get; set; }
        public Boardgames game { get; set; }
        public List<string> Expansions { get; set; }

        public List<Stat> Stats { get; set; }


        public List<Player> Players { get; set; }

        public List<Match_Stat> Match_Stats { get; set; }

        public string How_Win { get; set; }

        public string when { get; set; }
        public List<Stat> Game_Stats { get; set; }

    }
}
