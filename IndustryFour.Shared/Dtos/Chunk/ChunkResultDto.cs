using IndustryFour.Shared.Dtos.Document;

namespace IndustryFour.Shared.Dtos.Chunk
{
    public class ChunkResultDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string EmbeddedText { get; set; }
        public int DocumentId { get; set; }
    }
}
