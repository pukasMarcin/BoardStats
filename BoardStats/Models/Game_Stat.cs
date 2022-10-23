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
    public class Game_Stat
    {

        public int GameId { get; set; }
        public Boardgames Game { get; set; }

        public int StatId { get; set; }
        public Stat Stat { get; set; }


    }
}
