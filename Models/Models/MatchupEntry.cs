using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class MatchupEntry: ModelBase
    {
        /// <summary>
        /// Represents one team in the matchup
        /// </summary>
        public virtual Team TeamCompeting { get; set; }

        /// <summary>
        /// Represents the score for this particular team
        /// </summary>
        public double Score { get; set; }

        /// <summary>
        /// Represents the matchup that this team came from as the winner
        /// </summary>
        public virtual Matchup ParentMatchup { get; set; }

     
    }
}
