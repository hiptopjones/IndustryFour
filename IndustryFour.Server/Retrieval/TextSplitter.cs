namespace IndustryFour.Server.Retrieval
{
    public class TextSplitter : ITextSplitter
    {
        private string Separator { get; }
        private int ChunkSize { get; }
        private int ChunkOverlap { get; }

        public TextSplitter(string separator, int chunkSize, int chunkOverlap)
        {
            ArgumentException.ThrowIfNullOrEmpty(separator, nameof(separator));
            if (chunkSize <= 0) throw new ArgumentException("Chunk size must be positive and non-zero", nameof(chunkSize));
            if (chunkOverlap < 0) throw new ArgumentException("Chunk overlap must be positive", nameof(chunkOverlap));

            Separator = separator;
            ChunkSize = chunkSize;
            ChunkOverlap = chunkOverlap;
        }

        public async Task<IEnumerable<string>> Split(string text)
        {
            string[] splits = text.Split(Separator);
            return await MergeSplits(splits.ToList());
        }

        private async Task<List<string>> MergeSplits(List<string> splits)
        {
            List<string> chunks = new List<string>();

            List<string> currentChunkSplits = new List<string>();

            int splitIndex = 0;
            while (true)
            {
                currentChunkSplits.Add(splits[splitIndex]);
                splitIndex++;

                if (splitIndex < splits.Count)
                {
                    int currentTotalLength = currentChunkSplits.Sum(x => x.Length);
                    int nextTotalLength = currentTotalLength + currentChunkSplits.Count * Separator.Length;

                    if (nextTotalLength > ChunkSize)
                    {
                        string chunk = string.Join(Separator, currentChunkSplits);
                        chunks.Add(chunk);

                        // TODO: Update splitIndex

                        currentChunkSplits.Clear();
                    }
                }
                else
                {
                    string chunk = string.Join(Separator, currentChunkSplits);
                    chunks.Add(chunk);

                    // All done
                    break;
                }
            }

            return await Task.FromResult(chunks);
        }
    }

}