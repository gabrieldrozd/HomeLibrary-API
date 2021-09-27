using System.ComponentModel.DataAnnotations;

namespace HomeLibrary_API.DTOs.Book
{
    public class BookCreateDTO
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public string Summary { get; set; }
        public int? PublicationYear { get; set; }
    }
}
