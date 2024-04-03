using Newtonsoft.Json;
using Pgvector;

namespace IndustryFour.Server.Models;

public class Chunk : Entity
{
    public string Content { get; set; }

    [JsonIgnore]
    public Vector Embedding { get; set; }

    public int NextChunkId { get; set; }
    public int PreviousChunkId { get; set; }

    public int DocumentId { get; set; }

    // EF Relations
    public Document Document { get; set; }
}
        