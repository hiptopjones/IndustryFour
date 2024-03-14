using IndustryFour.Client.HttpRepository;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Headers;

namespace IndustryFour.Client.Components
{
	public partial class FileUpload
	{
		[Parameter]
		public string ContentUrl { get; set; }

		[Parameter]
		public EventCallback<string> OnChange { get; set; }

		[Inject]
		public IDocumentHttpRepository Repository { get; set; }

		private async Task HandleSelected(InputFileChangeEventArgs e)
		{
			var documentFile = e.File;
			if (documentFile == null)
			{
				return;
			}

			using (var stream = documentFile.OpenReadStream(documentFile.Size))
			{
				var content = new MultipartFormDataContent();
				content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");
				content.Add(new StreamContent(stream, Convert.ToInt32(documentFile.Size)), "document", documentFile.Name);

                ContentUrl = await Repository.UploadDocumentFile(content);

				await OnChange.InvokeAsync(ContentUrl);
			}
		}

	}
}
