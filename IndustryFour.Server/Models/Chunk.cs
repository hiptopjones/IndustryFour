using Pgvector;

namespace IndustryFour.Server.Models;

public class Chunk : Entity
{
    public string Content { get; set; }
    public Vector Embedding { get; set; }
    public int DocumentId { get; set; }

    // EF Relations
    public Document Document { get; set; }
}
        