namespace IndustryFour.Shared.Dtos.Document
{
    public class DocumentResultDto
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string Description { get; set; }
        public string Content { get; set; }

        public DateTime PublishDate { get; set; }
    }
}
