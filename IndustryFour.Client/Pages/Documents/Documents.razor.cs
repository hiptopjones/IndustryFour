using IndustryFour.Client.HttpInterceptor;
using IndustryFour.Client.HttpRepository;
using IndustryFour.Shared.Dtos.Document;
using Microsoft.AspNetCore.Components;

namespace IndustryFour.Client.Pages.Documents
{
    public partial class Documents : IDisposable
    {
        public List<DocumentResultDto> DocumentResults { get; set; } = new List<DocumentResultDto>();

        [Inject]
        public IDocumentHttpRepository DocumentRepository { get; set; }

        [Inject]
        public HttpInterceptorService Interceptor { get; set; }

        protected async override Task OnInitializedAsync()
        {
            Interceptor.RegisterEvent();

            await GetDocuments();
        }

        private async Task GetDocuments()
        {
            DocumentResults = await DocumentRepository.GetDocuments();
        }

        private async Task DeleteProduct(int id)
        {
            await DocumentRepository.DeleteDocument(id);
            await GetDocuments();
        }

        public void Dispose() => Interceptor.DisposeEvent();
    }
}
