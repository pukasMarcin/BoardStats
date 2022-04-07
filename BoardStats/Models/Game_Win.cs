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
    public class Game_Win
    {

        public int GameId { get; set; }
        public Boardgames Game { get; set; }

        public int WinConId{ get; set; }
        public WinCon WinCon { get; set; }


    }
}
