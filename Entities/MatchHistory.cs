namespace Jogo_de_Corrida_de_Dados.Entities
{
    internal class MatchHistory
    {
        public string Winner { get; set; }
        public int Matches { get; set; }
        public int MatchId { get; set; }
        public static int matchId = 0;
        public int CPUCount { get; set; }

        public MatchHistory() { }
        public MatchHistory(string winner, int matches, int cPUCount)
        {
            Winner = winner;
            Matches = matches;
            CPUCount = cPUCount;
            MatchId = ++matchId;
        }
    }
}