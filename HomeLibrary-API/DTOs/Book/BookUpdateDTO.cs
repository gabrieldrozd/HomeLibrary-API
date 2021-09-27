namespace HomeLibrary_API.DTOs.Book
{
    public class BookUpdateDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public string Summary { get; set; }
        public int? PublicationYear { get; set; }

        public int? AuthorId { get; set; }
    }
}
