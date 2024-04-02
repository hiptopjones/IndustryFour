using IndustryFour.Shared.Dtos.Chat;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
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
        private string Error { get; set; }
        private bool IsTaskRunning { get; set; }
        private bool IsSubmitDisabled => 
            IsTaskRunning || string.IsNullOrWhiteSpace(Request.Question);

        private Stopwatch Stopwatch { get; set; }

        private async Task Submit()
        {
            Logger.LogInformation("Submit clicked");

            Stopwatch = Stopwatch.StartNew();

            IsTaskRunning = true;
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

        private async Task OnPromptSubmitted(ChatRequestDto requestDto)
        {
            Logger.LogInformation("Submit handler started");

            try
            {
                var httpResponse = await HttpClient.PostAsJsonAsync("chat", requestDto);
                if (httpResponse.IsSuccessStatusCode)
                {
                    var response = await httpResponse.Content.ReadFromJsonAsync<ChatResponseDto>();
                    Messages.Last().AssistantMessage = response;

                    // Always transfer this across to track turns in a conversation
                    Request.ConversationId = response.ConversationId;
                }
                else
                {
                    Error = await httpResponse.Content.ReadAsStringAsync();
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

        private void OnNewChatClicked(MouseEventArgs e)
        {
            Messages.Clear();
            Request.ConversationId = 0;
        }
    }
}