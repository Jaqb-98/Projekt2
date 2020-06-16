using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Configurations
{
   public class TournamentConfiguration: EntityTypeConfiguration<Tournament>
    {
        public TournamentConfiguration()
        {
            ToTable("Tournaments");
            //HasKey(t => t.Id);

            Property(t => t.TournamentName).IsRequired().HasMaxLength(50);
            HasIndex(t => t.TournamentName).IsUnique();

            HasMany(t => t.Matchups)
                .WithRequired(m => m.Tournament)
                .WillCascadeOnDelete(true);
            
            
        }
    }
}
