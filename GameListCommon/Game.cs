using System;
using System.Collections.Generic;
using System.Text;

namespace GameListCommon
{
    public class Game
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public string ContestName { get; set; }
        public string HomeTeamName { get; set; }
        public string AwayTeamName { get; set; }
        public SportTypes SportType { get; set; }
        public string LeagueName { get; set; }
    }
}
