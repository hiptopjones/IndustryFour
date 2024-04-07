using Newtonsoft.Json;
using Pgvector;

namespace IndustryFour.Server.Models;

public class Chunk : Entity
{
    public string Content { get; set; }

    // The text used to generate the embedding is often different than the text provided for context

    [JsonIgnore] // Ignore when storing the chunks in chat history
    public string EmbeddedText { get; set; }

    [JsonIgnore] // Ignore when storing the chunks in chat history
    public Vector Embedding { get; set; }

    public int DocumentId { get; set; }

    // EF Relations
    public Document Document { get; set; }
}
        