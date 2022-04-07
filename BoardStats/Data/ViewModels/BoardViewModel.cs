using Microsoft.AspNetCore.Mvc.Rendering;
namespace BoardStats.Data.ViewModels
{
    public class BoardViewModel
    {


        public string BggId { get; set; }

        public string BggRate { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public int MinPlayers { get; set; }
        public int MaxPlayers { get; set; }

        public int BestPlayers { get; set; }

        public int PlayingTime { get; set; }

        public bool Expansion { get; set; }

        public string MainGame { get; set; }

        public string Category { get; set; }

        public bool IsInCollection { get; set; }

        public int OrderNumber { get; set; }

        public string InstructionUrl { get; set; }

        public List<int> Win_Ids { get; set; }

        public List<int> Stat_Ids { get; set; }
        public List<StatsVM> statsVM { get; set; }
    }
}
