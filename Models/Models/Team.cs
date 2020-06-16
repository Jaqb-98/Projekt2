using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Team : ModelBase
    {
        public string Name { get; set; }

        public virtual ICollection<Tournament> Tournaments { get; set; }
        public virtual ICollection<MatchupEntry> Entries { get; set; }

        public Team()
        {
            Tournaments = new HashSet<Tournament>();
            Entries = new HashSet<MatchupEntry>();
        }
    }
}
