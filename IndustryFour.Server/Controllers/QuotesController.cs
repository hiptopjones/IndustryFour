using IndustryFour.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace IndustryFour.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuotesController : ControllerBase
    {
        private readonly ILogger<QuotesController> _logger;

        private readonly QuotesService _quotes;

        public QuotesController(ILogger<QuotesController> logger, QuotesService quotes)
        {
            _logger = logger;
            _quotes = quotes;
        }

        [HttpGet]
        public IReadOnlyList<string> ListQuotes()
        {
            return _quotes.GetQuotes();
        }

        [HttpGet("random")]
        public string GetRandomQuote()
        {
            return _quotes.GetRandomQuote();
        }

        [HttpPost]
        public void CreateQuote(string quote)
        {
            _quotes.CreateQuote(quote);
        }
    }
}
