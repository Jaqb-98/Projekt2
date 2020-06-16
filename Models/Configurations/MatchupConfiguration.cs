using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Configurations
{
    public class MatchupConfiguration: EntityTypeConfiguration<Matchup>
    {
        public MatchupConfiguration()
        {
            ToTable("Matchups");
            //HasKey(m => m.Id);
            HasMany(m => m.Entries)
                .WithRequired(e => e.ParentMatchup).WillCascadeOnDelete(true);
    
        }
    }
}
