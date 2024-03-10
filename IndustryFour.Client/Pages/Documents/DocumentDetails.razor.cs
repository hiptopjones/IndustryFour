using IndustryFour.Client.HttpRepository;
using IndustryFour.Shared.Dtos.Document;
using Microsoft.AspNetCore.Components;

namespace IndustryFour.Client.Pages.Documents
{
    public partial class DocumentDetails
    {
        public DocumentResultDto Document { get; set; } = new DocumentResultDto();

        [Inject]
        public IDocumentHttpRepository DocumentRepository { get; set; }

        [Parameter]
        public int DocumentId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Document = await DocumentRepository.GetDocument(DocumentId);
        }
    }
}
