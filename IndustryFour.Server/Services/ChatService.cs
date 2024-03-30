using IndustryFour.Server.Models;
using IndustryFour.Server.Retrieval;
using System.Diagnostics;

namespace IndustryFour.Server.Services
{
    public class ChatService : IChatService
    {
        private readonly IDocumentIndexService _index;
        private readonly IChatProvider _chat;
        private readonly IConversationService _conversations;
        private readonly ITurnService _turns;

        public ChatService(
            IDocumentIndexService index,
            IChatProvider chat,
            IConversationService conversations,
            ITurnService turns)
        {
            _index = index;
            _chat = chat;
            _conversations = conversations;
            _turns = turns;
        }

        public async Task<ChatResponse> AskQuestion(ChatRequest request)
        {
            ChatResponse response = new ChatResponse();

            Turn turn = await CreateTurn(request, response);

            Stopwatch overallTimer = Stopwatch.StartNew();

            await SimilaritySearch(request, response);
            await GeneratePrompt(request, response);
            await SendPrompt(request, response);

            response.ResponseDuration = overallTimer.Elapsed;

            turn.Response = response;
            await _turns.Update(turn);

            return response;
        }

        private async Task SimilaritySearch(ChatRequest request, ChatResponse response)
        {
            Stopwatch searchTimer = Stopwatch.StartNew();

            var chunkMatches = await _index.SimilaritySearch(request.Question, request.MaxSearchResults != 0 ? request.MaxSearchResults : 1);
            response.ChunkMatches = chunkMatches.ToList();

            response.SimilaritySearchDuration = searchTimer.Elapsed;
        }

        private async Task GeneratePrompt(ChatRequest request, ChatResponse response)
        {
            var contextOnlyLimiter = request.UseContextOnly ? "If the answer is not in the provided context then just say that you don't know, don't try to make up an answer." : "";
            var conciseResponseLimiter = request.UseConciseResponse ? "Keep the answers short and to the point, unless asked to expand.\n" : "";
            var relevantChunks = string.Join("\n\n", response.ChunkMatches.Select(x => x.Chunk.Content));

            var prompt = $"""
                You are a helpful assistant.
                Use the following pieces of context to answer the question at the end.
                {contextOnlyLimiter}{conciseResponseLimiter}
                
                {relevantChunks}

                Question: {request.Question}
                Helpful Answer:
                """;
            response.Prompt = prompt;

            await Task.CompletedTask;
        }

        private async Task SendPrompt(ChatRequest request, ChatResponse response)
        {
            Stopwatch completionTimer = Stopwatch.StartNew();

            var answer = await _chat.Chat(response.Prompt);
            response.Answer = answer;

            response.ChatProviderDuration = completionTimer.Elapsed;
        }

        private async Task<Turn> CreateTurn(ChatRequest request, ChatResponse response)
        {
            Conversation conversation = await GetOrCreateConversation(request.ConversationId);
            response.ConversationId = conversation.Id;

            Turn turn = new Turn
            {
                Request = request,
                ConversationId = conversation.Id,
                Timestamp = DateTime.Now
            };
            await _turns.Add(turn);

            return turn;
        }

        private async Task<Conversation> GetOrCreateConversation(int conversationId)
        {
            Conversation conversation;

            if (conversationId == 0)
            {
                conversation = new Conversation
                {
                    Timestamp = DateTime.Now
                };
                await _conversations.Add(conversation);
            }
            else
            {
                conversation = await _conversations.GetById(conversationId);
            }

            return conversation;
        }
    }
}
