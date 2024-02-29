namespace IndustryFour.Server.Services
{
    public class QuotesService
    {
        private readonly List<string> _quotes = [
            "It does not matter how slowly you go as long as you do not stop.",
            "Our greatest glory is not in never falling, but in rising every time we fall.",
            "By three methods we may learn wisdom: First, by reflection, which is noblest; Second, by imitation, which is easiest; and third by experience, which is the bitterest.",
            "Everything has beauty, but not everyone sees it.",
            "He who knows all the answers has not been asked all the questions.",
            "Choose a job you love, and you will never have to work a day in your life.",
            "We are what we repeatedly do. Excellence, then, is not an act, but a habit.",
            "To see what is right and not to do it is want of courage, or of principle.",
            "The man who moves a mountain begins by carrying away small stones.",
            "Study the past if you would define the future.",
        ];

        public IReadOnlyList<string> GetQuotes()
        {
            return _quotes;
        }

        public string GetRandomQuote()
        {
            int index = Random.Shared.Next(_quotes.Count);
            return _quotes[index];
        }

        public void CreateQuote(string quote)
        {
            _quotes.Add(quote);
        }
    }
}
