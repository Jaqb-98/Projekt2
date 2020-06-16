using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Tournament : ModelBase
    {
        public string TournamentName { get; set; }
        public virtual ICollection<Team> EnteredTeams { get; set; }

        public virtual ICollection<Matchup> Matchups { get; set; }

        public Tournament()
        {
            EnteredTeams = new HashSet<Team>();
            Matchups = new HashSet<Matchup>();
        }
    }
}
