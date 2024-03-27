namespace IndustryFour.Server.Models;

public class Turn : Entity
{
    public ChatRequest Request { get; set; }
    public ChatResponse Response { get; set; }
    public DateTime Timestamp { get; set; }
    public int ConversationId { get; set; }

    // EF Relations
    public Conversation Conversation { get; set; }
}
