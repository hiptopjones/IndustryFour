namespace IndustryFour.Server.Models;

public class Category : Entity
{
    public string Name { get; set; }

    // EF Relations
    public IEnumerable<Document> Documents { get; set; }
}
