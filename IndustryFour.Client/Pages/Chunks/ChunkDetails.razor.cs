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

        protected override async Task OnInitializedAsync()
        {
            Chunk = await ChunkRepository.GetById(ChunkId);
            Document = await DocumentRepository.GetById(Chunk.DocumentId);
        }
    }
}
