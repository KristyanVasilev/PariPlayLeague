namespace PariPlayLeague.API.Contracts.Responses.Teams
{
    public record TeamTotalStatsResponse
    {
        public int Matches { get; init; }
        public int ScoredGoals { get; init; }
        public int ConsidedGoals { get; init; }
        public int YellowCards { get; init; }
        public int RedCards { get; init; }
        public int Fouls { get; init; }
    }
}
