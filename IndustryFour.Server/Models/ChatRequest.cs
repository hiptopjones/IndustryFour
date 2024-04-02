namespace IndustryFour.Server.Models
{
    public class ChatRequest
    {
        public string Question { get; set; }
        public string OriginalQuestion { get; set; }
        public int MaxSearchResults { get; set; } = 3;
        public bool UseContextOnly { get; set; } = true;
        public bool UseConciseResponse { get; set; } = true;

        public int ConversationId { get; set; }
    }
}
