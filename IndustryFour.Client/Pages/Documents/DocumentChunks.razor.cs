using IndustryFour.Client.HttpInterceptor;
using IndustryFour.Client.HttpRepository;
using IndustryFour.Shared.Dtos.Chunk;
using Microsoft.AspNetCore.Components;

namespace IndustryFour.Client.Pages.Documents
{
	public partial class DocumentChunks : IDisposable
	{
		[Parameter]
		public int DocumentId { get; set; }

		public List<ChunkResultDto> ChunkResults { get; set; }

        [Inject]
        public IChunkHttpRepository ChunkRepository { get; set; }

        [Inject]
        public HttpInterceptorService Interceptor { get; set; }

        protected async override Task OnInitializedAsync()
        {
            Interceptor.RegisterEvent();

            await GetChunks();
        }

        private async Task GetChunks()
        {
            ChunkResults = await ChunkRepository.GetByDocumentId(DocumentId);
        }

        public void Dispose() => Interceptor.DisposeEvent();
    }
}
