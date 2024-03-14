using System.ComponentModel.DataAnnotations;

namespace IndustryFour.Shared.Dtos.Document
{
    public class DocumentEditDto
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} is a required field")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "{0} is a required field")]
        [StringLength(150, ErrorMessage = "{0} must be between {2} and {1} characters", MinimumLength = 2)]
        public string Title { get; set; }

        [Required(ErrorMessage = "{0} is a required field")]
        [StringLength(150, ErrorMessage = "{0} must be between {2} and {1} characters", MinimumLength = 2)]
        public string Author { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "{0} is a required field")]
        public string ContentUrl { get; set; }

        [Required(ErrorMessage = "{0} is a required field")]
        public DateTime PublishDate { get; set; }
        public string SourceUrl { get; set; }
    }
}
