using System.ComponentModel.DataAnnotations;

namespace BoardStats.Data.ViewModels
{
    public class CalendarAppVM
    {

        public int IdGame { get; set; }
        public String MatchId { get; set; }
        public string GameName { get; set; }

      
        public String StartDate { get; set; }
        public bool isPlayed { get; set; }

    }
}
