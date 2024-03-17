namespace IndustryFour.Shared.Dtos.Chat;

public class ChatRequestDto
{
    public string Question { get; set; }
    public int MaxSearchResults { get; set; } = 3;
}
