using BoardStats.Models;

namespace BoardStats.Data.ViewModels
{
    public class GameStatsVM
    {
        public Boardgames Game { get; set; }

        public double avgDuration { get; set; }

        public int playedAmount { get; set; }

        public string bestWinner{ get; set; }
        public int avgNumberOfPlayers { get; set; }
        public List<AvgStatVM> avgStats { get; set; }

        public List<AvgStatVM> avgStatsNoPoints { get; set; }
        public List<AvgStatVM> avgWinStatsPoints { get; set; }
        public List<AvgStatVM> avgWinCon { get; set; }
        public bool isPartPoints { get; set; }
        public bool isSumPoints { get; set; }
        public bool isWinCon { get; set; }
        public bool isToDo{ get; set; }
        public bool isOnStart { get; set; }
        public double avgSumPoints { get; set; }

       
        public bool isToDo2 { get; set; }
        public bool isToDo3 { get; set; }
        public bool isToDo4 { get; set; }
        public List<AvgStatVM> top5Winners { get; set; }
        public List<AvgStatVM> playedExpansions { get; set; }

        public List<AvgStatVM> playersCounter { get; set; }
        public bool isOnStart2 { get; set; }
        public bool isOnStart3 { get; set; }
        public bool isOnStart4 { get; set; }
        public bool isExpansions { get; set; }

        public string ToDoName { get; set; }
        public string ToDoName2 { get; set; }
        public string ToDoName3 { get; set; }
        public string ToDoName4 { get; set; }
        public string OnStartName { get; set; }
        public string OnStartName2 { get; set; }
        public string OnStartName3 { get; set; }
        public string OnStartName4 { get; set; }



    }
}
