using AutoMapper;
using Blazored.Toast.Services;
using IndustryFour.Client.HttpInterceptor;
using IndustryFour.Client.HttpRepository;
using IndustryFour.Shared.Dtos.Document;
using Microsoft.AspNetCore.Components;

namespace IndustryFour.Client.Pages.Documents
{
    public partial class UpdateDocument : IDisposable
    {
        private DocumentEditDto _document;

        [Inject]
        public IDocumentHttpRepository DocumentRepository { get; set; }

        [Inject]
        public HttpInterceptorService Interceptor { get; set; }

        [Inject]
        public IToastService ToastService { get; set; }

        [Inject]
        public IMapper Mapper { get; set; }

        [Parameter]
        public int Id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            _document = Mapper.Map<DocumentEditDto>(
                await DocumentRepository.GetDocument(Id));

            Interceptor.RegisterEvent();
        }

        private async Task Update()
        {
            await DocumentRepository.UpdateDocument(_document);

            // Show a success message
            ToastService.ShowSuccess($"Action successful.  Document \"{_document.Title}\" successfully updated.");
        }

        private void AssignContentUrl(string contentUrl) => _document.ContentUrl = contentUrl;


		public void Dispose() => Interceptor.DisposeEvent();
    }
}
