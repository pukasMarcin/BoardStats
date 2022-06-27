namespace BoardStats.Models
{
    public class Match_Player
    {
        public int MatchId { get; set; }
        public Match Match { get; set; }

        public int PlayerId { get; set; }
        public Player Player { get; set; }

       
    }
}
