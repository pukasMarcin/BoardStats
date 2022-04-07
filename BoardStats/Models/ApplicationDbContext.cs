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
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game_Win>().HasKey(am => new
            {
                am.WinConId,
                am.GameId
            });

            modelBuilder.Entity<Game_Win> ().HasOne(m => m.Game).WithMany(am => am.Game_Win).HasForeignKey(m => m.GameId);
            modelBuilder.Entity<Game_Win>().HasOne(m => m.WinCon).WithMany(am => am.Game_Win).HasForeignKey(m => m.WinConId);


            modelBuilder.Entity<Game_Stat>().HasKey(am => new
            {
                am.StatId,
                am.GameId

            });

            modelBuilder.Entity<Game_Stat>().HasOne(m => m.Game).WithMany(am => am.Game_Stat).HasForeignKey(m => m.GameId);
            modelBuilder.Entity<Game_Stat>().HasOne(m => m.Stat).WithMany(am => am.Game_Stat).HasForeignKey(m => m.StatId);


            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Boardgames> BoardGames { get; set; }

        public virtual DbSet<Collection> Collections { get; set; }

        public virtual DbSet<WinCon> WinCons { get; set; }

        public virtual DbSet<Stat> Stats { get; set; }
        public virtual DbSet<Game_Win> Game_Wins { get; set; }
        public virtual DbSet<Game_Stat> Game_Stats { get; set; }


    }

    
}
