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
    public class Match_Expansion
    {
        public int MatchId { get; set; }
        public Match Match { get; set; }

        public int ExpansionId { get; set; }
        public Expansion Expansion { get; set; }
    }
}
