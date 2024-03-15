namespace IndustryFour.Server.Retrieval
{
    public interface IChatProvider
    {
        Task<string> Chat(string prompt);
    }
}
