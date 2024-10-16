﻿namespace PariPlayLeague.Domain.Entities
{
    public class TeamStatistic
    {
        public int Matches { get; set; }
        public int ScoredGoals { get; set; }
        public int ConsidedGoals { get; set; }
        public int YellowCards { get; set; }
        public int RedCards { get; set; }
        public int Fouls { get; set; }
        public int WonMatches { get; set; }
        public int DrawMatches { get; set; }
        public int LostMatches { get; set; }
    }
}
