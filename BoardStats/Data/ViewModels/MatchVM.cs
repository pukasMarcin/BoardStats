using BoardStats.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BoardStats.Data.ViewModels
{
    public class MatchVM
    {

        public int MatchId { get; set; }

        public int IdGame { get; set; }

        public string GameName { get; set; }

       
        public string StartDate { get; set; }

        public int Duration { get; set; }


        public string UserName { get; set; }

        public string UserId { get; set; }

        public string WhoWIn { get; set; }


        public int IdWinCon { get; set; }
   

        public List<Match_Stat> Match_Stat { get; set; }

        public List<MatchStatVM> MatchStatVM { get; set; }

        public List<PlayerVM> PlayerVM { get; set; }
        public List<PlayerVM> PlayerVM2 { get; set; }

        public List<ExpansionVM> ExpansionsVM { get; set; }
        public List<StatsVM> StatsVM { get; set; }

        public bool isChallange { get; set; }

        public int ChallangeId { get; set; }


    }
}
