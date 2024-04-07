using System.Text;

namespace IndustryFour.Server.Retrieval
{
    public class TextSplitter : ITextSplitter
    {
        private readonly ILogger<TextSplitter> _logger;

        private readonly string _separator;
        private readonly int _chunkSize;
        private readonly int _chunkOverlap;

        public TextSplitter(ILogger<TextSplitter> logger, string separator, int chunkSize, int chunkOverlap)
        {
            logger = logger ?? throw new ArgumentNullException(nameof(logger));
            ArgumentException.ThrowIfNullOrEmpty(separator, nameof(separator));
            if (chunkSize <= 0) throw new ArgumentException("Chunk size must be positive and non-zero", nameof(chunkSize));
            if (chunkOverlap < 0) throw new ArgumentException("Chunk overlap must be positive", nameof(chunkOverlap));

            _logger = logger;
            _separator = separator;
            _chunkSize = chunkSize;
            _chunkOverlap = chunkOverlap;
        }

        public Task<IEnumerable<string>> Split(string text)
        {
            _logger.LogInformation($"TextLength: {text.Length}");

            var chunks = new List<string>();
            var chunkBuilder = new StringBuilder();

            var startIndex = 0;

            while (startIndex < text.Length)
            {
                var endIndex = text.IndexOf(_separator, startIndex);
                if (endIndex < 0)
                {
                    // No more separators found, so take everything that remains
                    var segmentText = text.Substring(startIndex);
                    chunkBuilder.Append(segmentText);

                    var chunkText = chunkBuilder.ToString().Trim();
                    if (!string.IsNullOrEmpty(chunkText))
                    {
                        chunks.Add(chunkText);
                    }

                    break;
                }
                else
                {
                    endIndex = Math.Min(text.Length - 1, endIndex + _separator.Length);

                    var segmentText = text.Substring(startIndex, endIndex - startIndex);
                    chunkBuilder.Append(segmentText);

                    if (chunkBuilder.Length >= _chunkSize)
                    {
                        var chunkText = chunkBuilder.ToString().Trim();
                        if (!string.IsNullOrEmpty(chunkText))
                        {
                            chunks.Add(chunkText);
                        }

                        chunkBuilder.Clear();
                    }

                    startIndex = endIndex;
                }
            }

            return Task.FromResult(chunks.AsEnumerable());
        }
    }
}