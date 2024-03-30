namespace IndustryFour.Server.Models
{
    public class ChatResponse
    {
        public string Answer { get; set; }
        public List<ChunkMatch> ChunkMatches { get; set; }
        public string Prompt { get; set; }
        public TimeSpan SimilaritySearchDuration { get; set; }
        public TimeSpan ChatProviderDuration { get; set; }
        public TimeSpan ResponseDuration { get; set; }

        public int ConversationId { get; set; }
    }
}
