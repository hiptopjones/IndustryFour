namespace IndustryFour.Server.Models;

public class Document : Entity
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string Description { get; set; }
    public string ContentUrl { get; set; }
    public string SourceUrl { get; set; }
    public DateTime PublishDate { get; set; }
    public int CategoryId { get; set; }

    // EF Relations
    public Category Category { get; set; }
}
