namespace BoardStats.Data.ViewModels
{
    public class PlayerVM
    {
        public int HoldId { get; set; }

        public string PlayerName { get; set; }

        public bool isChecked { get; set; }
        public List<StatsVM> StatsVM { get; set; }
    }
}
