
using BoardStats.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;




namespace BoardStats.Data.ViewModels
{
    public class NewGameVM
    {
        public NewGameVM()
        {
            WinCons = new List<WinCon>();
            Stats = new List<Stat>();
            isChecked = false;
         
        }

        public List<WinCon> WinCons { get; set; }
        public List<Stat> Stats { get; set; }

        public bool isChecked { get; set; }
        

    }
}
