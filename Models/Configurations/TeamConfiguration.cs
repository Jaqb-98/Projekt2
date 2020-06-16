using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Models.Configurations
{
    public class TeamConfiguration: EntityTypeConfiguration<Team>
    {
        public TeamConfiguration()
        {
            ToTable("Teams");
           // HasKey(t => t.Id);

            Property(t => t.Name).IsRequired().HasMaxLength(50);
            HasIndex(t => t.Name).IsUnique();

            HasMany(t => t.Tournaments)
                  .WithMany(t => t.EnteredTeams);

        }
    }
}
