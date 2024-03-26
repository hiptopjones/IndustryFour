using IndustryFour.Client.HttpInterceptor;
using IndustryFour.Client.HttpRepository;
using IndustryFour.Shared.Dtos.Document;
using Microsoft.AspNetCore.Components;

namespace IndustryFour.Client.Pages.Documents
{
    public partial class Documents : IDisposable
    {
        public List<DocumentResultDto> DocumentResults { get; set; }

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
            DocumentResults = await DocumentRepository.GetAll();
        }

        private async Task DeleteProduct(int id)
        {
            await DocumentRepository.Delete(id);
            await GetDocuments();
        }

        public void Dispose() => Interceptor.DisposeEvent();
    }
}
