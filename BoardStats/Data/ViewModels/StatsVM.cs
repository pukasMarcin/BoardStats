namespace BoardStats.Data.ViewModels
{
    public class StatsVM
    {
       
        public int StatsId { get; set; }

        public string Statistic { get; set; }

        public string Category { get; set; }
        public bool isChecked { get; set; }
        public string Value { get; set; }
    }
}
