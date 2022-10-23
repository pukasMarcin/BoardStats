using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoardStats.Models
{
    public class Expansion
    {
        [Key]
        public int ExpansionId { get; set; }

        public int IdGame { get; set; }
        [ForeignKey("IdGame")]


        public List<Match_Expansion> Match_Expansion { get; set; }
    }
}
