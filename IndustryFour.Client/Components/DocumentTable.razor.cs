using IndustryFour.Shared.Dtos.Document;
using Microsoft.AspNetCore.Components;

namespace IndustryFour.Client.Components
{
    public partial class DocumentTable
	{
		[Parameter]
		public List<DocumentResultDto> Documents { get; set; }

		[Parameter]
		public EventCallback<int> OnDelete { get; set; }

		private Confirmation _confirmation;
		private int _documentIdToDelete;

		private void CallConfirmationModal(int id)
		{
			_documentIdToDelete = id;
			_confirmation.Show();
		}

		private async Task DeleteDocument()
		{
			_confirmation.Hide();
			await OnDelete.InvokeAsync(_documentIdToDelete);
		}
	}
}
