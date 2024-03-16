using Microsoft.AspNetCore.Components;

namespace IndustryFour.Client.Pages
{
    public partial class About
    {
        [Inject]
        private HttpClient HttpClient { get; set; }

        private string Quote { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Quote = await HttpClient.GetStringAsync("quotes/random");
        }
    }
}