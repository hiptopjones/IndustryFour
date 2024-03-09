using IndustryFour.Client.HttpRepository;
using IndustryFour.Shared.Dtos.Document;
using Microsoft.AspNetCore.Components;

namespace IndustryFour.Client.Pages
{
    public partial class Documents
    {
        public List<DocumentResultDto> DocumentResults { get; set; } = new List<DocumentResultDto>();

        [Inject]
        public IDocumentHttpRepository DocumentRepository { get; set; }

        protected async override Task OnInitializedAsync()
        {
            DocumentResults = await DocumentRepository.GetDocuments();

            foreach (var document in DocumentResults)
            {
                Console.WriteLine(document.Title);
            }
        }
    }
}
