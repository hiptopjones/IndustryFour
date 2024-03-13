using Blazored.Toast.Services;
using IndustryFour.Client.HttpInterceptor;
using IndustryFour.Client.HttpRepository;
using IndustryFour.Shared.Dtos.Document;
using Microsoft.AspNetCore.Components;

namespace IndustryFour.Client.Pages.Documents
{
    public partial class CreateDocument : IDisposable
    {
        private DocumentAddDto _document = new DocumentAddDto();

        [Inject]
        public IDocumentHttpRepository DocumentRepository { get; set; }

        [Inject]
        public HttpInterceptorService Interceptor { get; set; }

        [Inject]
        public IToastService ToastService { get; set; }

        protected override void OnInitialized()
        {
            Interceptor.RegisterEvent();
        }

        private async Task Create()
        {
            await DocumentRepository.CreateDocument(_document);

            // Show a success message
            ToastService.ShowSuccess($"Action successful.  Document \"{_document.Title}\" successfully added.");

            // Clear the form
            _document = new DocumentAddDto();
        }

        private void AssignContentUrl(string contentUrl) => _document.ContentUrl = contentUrl;


		public void Dispose() => Interceptor.DisposeEvent();
    }
}
