namespace IndustryFour.Shared.Dtos.Chat;

public class ChatRequestDto
{
    public string Question { get; set; }
    public int MaxSearchResults { get; set; } = 3;
    public bool UseContextOnly { get; set; } = true;
    public bool UseConciseResponse { get; set; } = true;
}
