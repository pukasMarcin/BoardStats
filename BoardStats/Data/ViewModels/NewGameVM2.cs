
using BoardStats.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;




namespace BoardStats.Data.ViewModels
{
    public class NewGameVM2
    {
        public NewGameVM2()
        {
            WinCons = new List<WinVM>();
            Stats = new List<StatsVM>();

        }

        public List<WinVM> WinCons { get; set; }
        public List<StatsVM> Stats { get; set; }


    }
}
