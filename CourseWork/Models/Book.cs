using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Avalonia.Media.Imaging;

namespace CourseWork.Models
{
    public class Book
    {
        public long Id { get; set; }
        public int NumberOfPages { get; set; } = 0;
        [Required]
        public string? Name { get; set; }
        public string? Publisher { get; set; }
        public string Isbn10 { get; set; } = "";
        public string Isbn13 { get; set; } = "";
        public IBitmap? Cover { get; set; }
        public bool IsPresent { get; set; } = true;
        public int PublishingYear { get; set; } = 0;
        public IList<Author> Authors { get; set; } = new List<Author>();
        public IList<Subject> Subjects { get; set; } = new List<Subject>();
    }
}