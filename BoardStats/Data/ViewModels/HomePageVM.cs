using BoardStats.Models;
using BoardStats.Data;

namespace BoardStats.Data.ViewModels
{
    public class HomePageVM
    {
        public string gameName { get; set; }
        public string whoWin { get; set; }
        public string imgUrl { get; set; }
        public int duration { get; set; }


        public string when { get; set; }

        public int IdGame { get; set; }
        public String MatchId { get; set; }
        public string GameName { get; set; }

        public List<MatchVM> last10Games{get;set;}
        public String StartDate { get; set; }
        public bool isPlayed { get; set; }


    }
}
