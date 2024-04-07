using IndustryFour.Shared.Dtos.Chat;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
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

        [Inject]
        private IWebAssemblyHostEnvironment HostEnvironment { get; set; }

        // TODO: Populate from database, maybe using real questions
        private List<string> SampleQuestions { get; set; } = new List<string>
        {   
            "How do I get started with digital transformation?",
            //"How can I start collecting data?", // no good answers with the current data
            "What is an IIoT platform?",
            "What is the automation stack?",
            "What is an Industry 4.0 company?",
            "What are the problems associated with digital thread architecture?",
        };

        private List<ChatMessage> Messages { get; set; } = new List<ChatMessage>();
        private ChatRequestDto Request { get; set; } = new ChatRequestDto();
        private string Error { get; set; }
        private bool IsTaskRunning { get; set; }
        private bool IsSubmitDisabled => 
            IsTaskRunning || string.IsNullOrWhiteSpace(Request.Question);

        private Stopwatch Stopwatch { get; set; }

        private InputText QuestionInput { get; set; }

        protected override void OnInitialized()
        {
            Request.UseCache = HostEnvironment.IsDevelopment();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (QuestionInput.Element.HasValue)
            {
                await QuestionInput.Element.Value.FocusAsync();
            }
        }

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

            // Copy and then clear the request (maintaining any important settings)
            var request = Request;
            Request = new ChatRequestDto();
            Request.UseCache = request.UseCache;

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

        private async Task OnSampleQuestion(string question)
        {
            Request.Question = question;
            await Submit();
        }
    }
}