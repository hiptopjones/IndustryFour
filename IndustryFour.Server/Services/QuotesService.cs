using Newtonsoft.Json.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Threading;

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
            "I would rather have questions that can't be answered than answers that can't be questioned.",
            "If you think you understand quantum mechanics, you don't understand quantum mechanics.",
            "The first principle is that you must not fool yourself — and you are the easiest person to fool.",
            "Science is the belief in the ignorance of experts.",
            "What I cannot create, I do not understand.",
            "It's not what we don't know that gets us into trouble. It's what we know for sure that just ain't so.",
            "You can know the name of a bird in all the languages of the world, but when you're finished, you'll know absolutely nothing whatever about the bird. So let's look at the bird and see what it's doing — that's what counts.",
            "Physics is like sex: sure, it may give some practical results, but that's not why we do it.",
            "It doesn't matter how beautiful your theory is, it doesn't matter how smart you are. If it doesn't agree with experiment, it's wrong.",
            "The pleasure of finding things out is more important than the pleasure of getting new facts.",
            "The important thing is not to stop questioning. Curiosity has its own reason for existing.",
            "Ignorance more frequently begets confidence than does knowledge.",
            "Doubt is not a pleasant condition, but certainty is absurd.",
            "Research is what I'm doing when I don't know what I'm doing.",
            "To know, is to know that you know nothing. That is the meaning of true knowledge.",
            "The art of teaching is the art of assisting discovery.",
            "Not all those who wander are lost.",
            "The only true wisdom is in knowing you know nothing.",
            "In the fields of observation chance favors only the prepared mind.",
            "I have not failed. I've just found 10,000 ways that won't work.",
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
