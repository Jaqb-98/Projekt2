using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Configurations
{
    public class MatchupEntryConfiguration: EntityTypeConfiguration<MatchupEntry>
    {
        public MatchupEntryConfiguration()
        {
            ToTable("MatchupEntries");
            // HasKey(m => m.Id);

            HasRequired(e => e.ParentMatchup)
                .WithMany(pm => pm.Entries);


        }
    }
}
