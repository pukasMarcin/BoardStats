namespace BoardStats.Data.ViewModels
{
    public class AvgStatVM
    {
        public string Stat { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public int ValueI { get; set; }
        public string ValueS { get; set; }
        public int ValueSAmount { get; set; }
        public decimal ValueSAvg { get; set; }
        public int StatId{ get; set; }
        public int MatchId { get; set; }
        public int PlayerId { get; set; }
        public decimal avgForWin { get; set; }
       
     


    }
}
