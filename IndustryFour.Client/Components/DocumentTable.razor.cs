using IndustryFour.Shared.Dtos.Document;
using Microsoft.AspNetCore.Components;

namespace IndustryFour.Client.Components
{
    public partial class DocumentTable
	{
		[Parameter]
		public List<DocumentResultDto> Documents { get; set; }
	}
}
