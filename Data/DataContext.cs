using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Models.Configurations;

namespace Data
{
    public class DataContext : DbContext
    {
        public DbSet<Team> Teams { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<Matchup> Matchups { get; set; }
        public DbSet<MatchupEntry> MatchupEntries { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new TeamConfiguration());
            modelBuilder.Configurations.Add(new TournamentConfiguration());
            modelBuilder.Configurations.Add(new MatchupConfiguration());
            modelBuilder.Configurations.Add(new MatchupEntryConfiguration());
            
           // Configuration.ProxyCreationEnabled = true;
        }
    }
}
