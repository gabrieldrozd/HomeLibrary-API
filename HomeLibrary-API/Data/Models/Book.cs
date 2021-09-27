using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HomeLibrary_API.Data.Models
{
    [Table("Books")]
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public string Summary { get; set; }
        public int? PublicationYear { get; set; }
        
        public int? AuthorId { get; set; }
        public virtual Author Author { get; set; }
    }
}
