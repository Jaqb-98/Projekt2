using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Matchup : ModelBase
    {

        /// <summary>
        /// The set of teams that were involved in this match
        /// </summary>
        public virtual ICollection<MatchupEntry> Entries { get; set; }
        /// <summary>
        /// The winner of the match
        /// </summary>
        public virtual Team Winner { get; set; }
        /// <summary>
        /// Which round this match is a part of
        /// </summary>
        public int MatchupRound { get; set; }

        public virtual Tournament Tournament { get; set; }

        public Matchup()
        {
            Entries = new HashSet<MatchupEntry>();
        }
    }
}
