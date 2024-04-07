using IndustryFour.Client.HttpRepository;
using IndustryFour.Shared.Dtos.Chunk;
using IndustryFour.Shared.Dtos.Document;
using Microsoft.AspNetCore.Components;

namespace IndustryFour.Client.Pages.Chunks
{
    public partial class ChunkDetails
    {
        public ChunkResultDto Chunk { get; set; } = new ChunkResultDto();
        public DocumentResultDto Document { get; set; } = new DocumentResultDto();

        [Inject]
        public IChunkHttpRepository ChunkRepository { get; set; }

        [Inject]
        public IDocumentHttpRepository DocumentRepository { get; set; }

        [Parameter]
        public int ChunkId { get; set; }

        // MarkupString so we can embed HTML
        private MarkupString ChunkContentWithEmbeddedText { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Chunk = await ChunkRepository.GetById(ChunkId);
            Document = await DocumentRepository.GetById(Chunk.DocumentId);

            // Highlight the embedded text inside of the content
            var chunkContent = Chunk.Content.Replace(Chunk.EmbeddedText, $"<b>{Chunk.EmbeddedText}</b>");
            ChunkContentWithEmbeddedText = new MarkupString(chunkContent);
        }
    }
}
