using HomeLibrary_API.DTOs.Author;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeLibrary_API.DTOs.Book
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public string Summary { get; set; }
        public int? PublicationYear { get; set; }

        public int? AuthorId { get; set; }
        public virtual AuthorDTO Author { get; set; }
    }
}
