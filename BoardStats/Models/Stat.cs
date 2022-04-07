using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BoardStats.Models
{
    public class Stat
    {

        [Key]
        public int IdStat { get; set; }

        public string Statistic { get; set; }
        
        public string StatCategory { get; set; }
        public List<Game_Stat> Game_Stat { get; set; }
    }
}
