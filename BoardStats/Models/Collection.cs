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
    public class Collection
    {
        [Key]
        public int IdColl { get; set; }


        [Display(Name = "Boardgames")]
        public virtual int IdGame { get; set; }

        [ForeignKey("IdGame")]
        public virtual Boardgames GameId { get; set; }

        public string UserId { get; set; }

        public bool IsCollection { get; set; }
        
        
    }
}
