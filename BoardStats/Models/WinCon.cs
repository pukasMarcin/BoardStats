
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
    public class WinCon
    {

        [Key]
        public int IdWinCon{ get; set; }

        public string WinCondition { get; set; }

        public List<Game_Win> Game_Win { get; set; }
    }
}
