namespace BoardStats.Models
{
    public class Match_Stat
    {
        public int MatchId { get; set; }
        public Match Match { get; set; }

        public int PlayerId { get; set; }
        public Player Player { get; set; }

        public int IdStat { get; set; }
        public Stat Stat { get; set; }

        public string Value { get; set; }

    }
}
