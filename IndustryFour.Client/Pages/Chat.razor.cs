using IndustryFour.Shared.Dtos.Chat;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace IndustryFour.Client.Pages
{
    public partial class Chat
    {
        [Inject]
        private HttpClient HttpClient { get; set; }

        [Inject]
        protected IJSRuntime _JsRuntime { get; set; }

        private ChatRequestDto Request { get; set; } = new ChatRequestDto();
        private ChatResponseDto Response { get; set; }
        private string Error { get; set; }
        private bool IsTaskRunning { get; set; }

        private async Task Submit()
        {
            IsTaskRunning = true;
            Response = null;
            Error = null;

            // Forces a render
            await Task.Delay(1);

            await OnPromptSubmitted();

            Request = new ChatRequestDto();

            IsTaskRunning = false;
        }

        private async Task OnPromptSubmitted()
        {
            try
            {
                var response = await HttpClient.PostAsJsonAsync("chat", Request);
                if (response.IsSuccessStatusCode)
                {
                    Response = await response.Content.ReadFromJsonAsync<ChatResponseDto>();
                }
                else
                {
                    Error = await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                Error = ex.ToString();
            }
        }
    }
}