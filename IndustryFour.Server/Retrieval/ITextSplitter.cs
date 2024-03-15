namespace IndustryFour.Server.Retrieval
{
    public interface ITextSplitter
    {
        Task<IEnumerable<string>> Split(string text);
    }
}