namespace IndustryFour.Server.Models
{
    public class Question : Entity
    {
        public string QuestionText { get; set; }
        public string AnswerText { get; set; }
        public DateTime Timestamp { get; set; }
        public int TurnId { get; set; }

        // EF Relations
        public Turn Turn { get; set; }
    }
}
