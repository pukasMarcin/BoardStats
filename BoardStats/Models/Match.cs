using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoardStats.Models
{
    public class Match
    {
        [Key]
        public int MatchId { get; set; }

      
        public int IdGame { get; set; }

        [ForeignKey(nameof(IdGame))]
        public Boardgames Game { get; set; }

        public string GameName { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate{ get; set; }

        public int Duration { get; set; }


        public string UserName { get; set; }

        public string UserId { get; set; }

        public string WhoWIn { get; set; }

        public bool isChallange { get; set; }
        public int ChallangeId { get; set; }

        [ForeignKey(nameof(ChallangeId))]
        public Challange Challange { get; set; }
        public int IdWinCon { get; set; }
        [ForeignKey(nameof(IdWinCon))]
        public WinCon win { get; set; }

        public List<Match_Stat> Match_Stat{ get; set; }

        public List<Match_Player> Match_Player { get; set; }

        public int IdWinner { get; set; }
    }
}
