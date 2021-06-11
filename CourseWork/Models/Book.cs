using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Avalonia.Media.Imaging;

namespace CourseWork.Models
{
    public class Book
    {
        public long Id { get; set; }
        public int NumberOfPages { get; set; }

        [Required] public string? Name { get; set; }

        public string? Publisher { get; set; }
        public IBitmap? Cover { get; set; }

        [NotMapped] public string? CoverUrl { get; init; }

        public bool IsPresent { get; set; } = true;
        public int PublishingYear { get; set; }
        public IList<Author> Authors { get; init; } = new List<Author>();
        public IList<Subject> Subjects { get; init; } = new List<Subject>();
    }
}