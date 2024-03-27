using AutoMapper;
using IndustryFour.Server.Models;
using IndustryFour.Server.Services;
using IndustryFour.Shared.Dtos.Chat;
using Microsoft.AspNetCore.Mvc;

namespace GenTools.Server.Controllers
{
    [ApiController]
    [Route("api/chat")]
    public class ChatController : ControllerBase
    {
        private readonly ILogger<ChatController> _logger;
        private readonly IChatService _chat;
        private readonly IMapper _mapper;

		public ChatController(
            ILogger<ChatController> logger,
            IChatService chat,
            IMapper mapper)
        {
            _logger = logger;
            _chat = chat;
            _mapper = mapper;
        } 

        [HttpPost]
        public async Task<IActionResult> AskQuestion(ChatRequestDto requestDto)
        {
            var request = _mapper.Map<ChatRequest>(requestDto);
            var response = await _chat.AskQuestion(request);
            var responseDto = _mapper.Map<ChatResponseDto>(response);

            return Ok(responseDto);
        }
    }
}
