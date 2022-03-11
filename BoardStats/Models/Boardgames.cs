
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
    public class Boardgames
    {
        [Key]
        public int IdGame{ get; set; }

        public string BggId { get; set; }

        public string BggRate { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl{ get; set; }

        public int MinPlayers { get; set; }
        public int MaxPlayers { get; set; }

        public int BestPlayers { get; set; }

        public int PlayingTime { get; set; }

        public bool Expansion { get; set; }

        public string MainGame { get; set; }

        public string Category { get; set; }

        public bool IsInCollection { get; set; }
    }
}
