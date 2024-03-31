using IndustryFour.Shared.Dtos.Chat;
using Microsoft.AspNetCore.Components;
using System.Diagnostics;
using System.Net.Http.Json;

namespace IndustryFour.Client.Pages.Chat
{
    public partial class Chat
    {
        [Inject]
        private ILogger<Chat> Logger { get; set; }

        [Inject]
        private HttpClient HttpClient { get; set; }

        private List<ChatMessage> Messages { get; set; } = new List<ChatMessage>();
        private ChatRequestDto Request { get; set; } = new ChatRequestDto();
        private ChatResponseDto Response { get; set; }
        private string Error { get; set; }
        private bool IsTaskRunning { get; set; }

        private Stopwatch Stopwatch { get; set; }

        private async Task Submit()
        {
            Logger.LogInformation("Submit clicked");

            Stopwatch = Stopwatch.StartNew();

            IsTaskRunning = true;
            Response = null;
            Error = null;

            Messages.Add(new ChatMessage
            {
                IsUserMessage = true,
                UserMessage = Request
            });

            Messages.Add(new ChatMessage
            {
                IsUserMessage = false
            });

            // Copy and then clear the request
            var request = Request;
            Request = new ChatRequestDto();

            // Forces a render so the button gets disabled immediately
            await Task.Delay(1);

            await OnPromptSubmitted(request);

            IsTaskRunning = false;

            Logger.LogInformation("Submit end");
        }

        private async Task OnPromptSubmitted(ChatRequestDto request)
        {
            Logger.LogInformation("Submit handler started");

            try
            {
                var response = await HttpClient.PostAsJsonAsync("chat", request);
                if (response.IsSuccessStatusCode)
                {
                    Response = await response.Content.ReadFromJsonAsync<ChatResponseDto>();
                    Messages.Last().AssistantMessage = Response;

                    // Always transfer this across to track turns in a conversation
                    Request.ConversationId = Response.ConversationId;
                }
                else
                {
                    Error = await response.Content.ReadAsStringAsync();
                    Messages.Last().ErrorMessage = Error;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error during submit handler");

                Error = ex.ToString();
                Messages.Last().ErrorMessage = Error;
            }
            finally
            {
                Stopwatch.Stop();
            }

            Logger.LogInformation("Submit handler ended ");
        }
    }
}