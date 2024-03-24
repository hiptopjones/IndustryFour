using IndustryFour.Shared.Dtos.Chunk;

namespace IndustryFour.Shared.Dtos.Chat;

public class ChatResponseDto
{
    public string Answer { get; set; }

    public ChatRequestDto Request { get; set; }
    public List<ChunkResultDto> Chunks { get; set; }
    public string Prompt { get; set; }
    public TimeSpan SimilaritySearchDuration { get; set; }
	public TimeSpan ChatProviderDuration { get; set; }
}
